using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Brands.Queries.GetDynamic;

public class GetDynamicQuery : IRequest<GetListResponse<GetDynamicResponse>>
{
    public PageRequest PageRequest { get; set; }

    public DynamicQuery DynamicQuery { get; set; }

    public class GetDynamicQueryHandler : IRequestHandler<GetDynamicQuery, GetListResponse<GetDynamicResponse>>
    {
        private readonly IMapper _mapper;
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly IBrandRepository _brandRepository;

        public GetDynamicQueryHandler(IMapper mapper, BrandBusinessRules brandBusinessRules, IBrandRepository brandRepository)
        {
            _mapper = mapper;
            _brandBusinessRules = brandBusinessRules;
            _brandRepository = brandRepository;
        }

        public async Task<GetListResponse<GetDynamicResponse>> Handle(GetDynamicQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Brand> brands = await _brandRepository.GetListByDynamicAsync(
                dynamic: request.DynamicQuery, 
                index: request.PageRequest.PageIndex, 
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken);


            GetListResponse<GetDynamicResponse> response = _mapper.Map<GetListResponse<GetDynamicResponse>>(brands);
            return response;
        }
    }
}
