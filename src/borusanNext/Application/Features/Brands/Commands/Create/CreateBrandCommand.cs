using Application.Features.Brands.Constants;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Brands.Constants.BrandsOperationClaims;
using Microsoft.AspNetCore.Http;
using Application.Services.ImageService;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using Microsoft.AspNetCore.SignalR;
using Application.Hubs;
using NArchitecture.Core.ElasticSearch;
using NArchitecture.Core.ElasticSearch.Models;

namespace Application.Features.Brands.Commands.Create;


// 10:25
public class CreateBrandCommand : IRequest<CreatedBrandResponse>, ICacheRemoverRequest, ILoggableRequest
{
    public string Name { get; set; }
    public IFormFile Logo { get; set; }
    public bool BypassCache => false;

    public string? CacheKey { get; }

    public string[]? CacheGroupKey => new string[] { "Brand.Get" };

    //public string[] Roles => [Admin, Write, BrandsOperationClaims.Create];

    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, CreatedBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;
        private readonly ImageServiceBase _imageServiceBase;
        private readonly IHubContext<ChatHub> _chatHub;
        private readonly IElasticSearch _elasticSearch;

        public CreateBrandCommandHandler(IMapper mapper, IBrandRepository brandRepository,
                                         BrandBusinessRules brandBusinessRules, ImageServiceBase imageServiceBase, IHubContext<ChatHub> chatHub, IElasticSearch elasticSearch)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
            _imageServiceBase = imageServiceBase;
            _chatHub = chatHub;
            _elasticSearch = elasticSearch;
        }
        // 10:15 -> Dynamic Query
        public async Task<CreatedBrandResponse> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            Brand brand = _mapper.Map<Brand>(request);

            brand.Logo = await _imageServiceBase.UploadAsync(request.Logo);

            await _brandRepository.AddAsync(brand);

            await _chatHub.Clients.All.SendAsync("NewBrandCreated", brand);

            //await _elasticSearch.CreateNewIndexAsync(new IndexModel() { IndexName = "brands", AliasName = "brandTable" });



            await _elasticSearch.InsertAsync(new ElasticSearchInsertUpdateModel(brand) { 
                IndexName = "brands", 
                Item = brand,
                ElasticId=brand.Id
            });

            CreatedBrandResponse response = _mapper.Map<CreatedBrandResponse>(brand);
            return response;
        }
    }
}