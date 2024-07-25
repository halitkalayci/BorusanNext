using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Domain.Entities;
using Hangfire;
using MediatR;
using MimeKit;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Mailing;
using NArchitecture.Core.Security.Entities;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>, ITransactionalRequest
{
    public UserForRegisterDto UserForRegisterDto { get; set; }
    public string IpAddress { get; set; }

    public RegisterCommand()
    {
        UserForRegisterDto = null!;
        IpAddress = string.Empty;
    }

    public RegisterCommand(UserForRegisterDto userForRegisterDto, string ipAddress)
    {
        UserForRegisterDto = userForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly AuthBusinessRules _authBusinessRules;
        private readonly IMailService _mailService;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IAuthService authService,
            AuthBusinessRules authBusinessRules,
            IMailService mailService)
        {
            _userRepository = userRepository;
            _authService = authService;
            _authBusinessRules = authBusinessRules;
            _mailService = mailService;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.UserForRegisterDto.Email);

            HashingHelper.CreatePasswordHash(
                request.UserForRegisterDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User newUser =
                new()
                {
                    Email = request.UserForRegisterDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            User createdUser = await _userRepository.AddAsync(newUser);


            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);



            BackgroundJob.Schedule(() => SendMail(createdUser.Email), TimeSpan.FromDays(3).Add(TimeSpan.FromHours(-3)));


            RecurringJob.AddOrUpdate("myJob", () => SendMail(createdUser.Email), "0 0 0-10 ? * * *");

            // 2. Pair GÖREVİ:
            // Her gün saat öğle 12'de tüm kullanıcılara mail gönderen bir bg job yazın. Mail html formatında güzel bir template ile son eklenen 3 arabayı göndersin..


            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }

        public void SendMail(string email)
        {
            var toEmailList = new List<MailboxAddress> { new(name: email, email) };

            // 1. Yöntem => HTML'i direkt koda yazmak.
            // 2. Yöntem => HTML'i databasede tutmak
            // 3. Yöntem => HTML'i dosyada, dosya yolunu databasede tutmak.
            string filePath = @"C:\Users\klyyc\Desktop\Projects\NET\BorusanNext\mail.html";

            string html = File.ReadAllText(filePath);

            html = html.Replace("{{confirmationLink}}", "https://github.com/halitkalayci/BorusanNext");

            _mailService.SendMail(new Mail()
            {
                ToList = toEmailList,
                Subject = "Borusan Next'e Hoş Geldin!",
                HtmlBody = html
            });
        }
    }
}
