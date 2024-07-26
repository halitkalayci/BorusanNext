using Application.Features.Brands.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.Brands.Constants.BrandsOperationClaims;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.CrossCuttingConcerns.Logging.Abstraction;
using NArchitecture.Core.Application.Pipelines.Performance;
using NArchitecture.Core.ElasticSearch;
using NArchitecture.Core.ElasticSearch.Models;

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<List<Brand>>, IIntervalRequest
{
    public PageRequest PageRequest { get; set; }

    public bool BypassCache => false;

    // GetListBrandQuery
    // GetListBrandQuery.PageIndex=0&PageSize=50
    public string CacheKey => $"GetListBrandQuery.PageIndex={PageRequest.PageIndex}&PageSize={PageRequest.PageSize}";

    public string? CacheGroupKey => "Brand.Get";

    public TimeSpan? SlidingExpiration => TimeSpan.FromHours(12);

    public int Interval => 10;

    //public TimeSpan? SlidingExpiration { get; }; // => Global deðeri kullan.


    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, List<Brand>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IElasticSearch _elasticSearch;

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, ILogger logger, IElasticSearch elasticSearch)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
            _elasticSearch = elasticSearch;
        }

        public async Task<List<Brand>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            //IPaginate<Brand> brands = await _brandRepository.GetListAsync(
            //    index: request.PageRequest.PageIndex,
            //    size: request.PageRequest.PageSize, 
            //    cancellationToken: cancellationToken
            //);
      
            _logger.Warning("Bu bir deneme mesajýdýr.");

            var brandsFromElastic = await _elasticSearch.GetSearchBySimpleQueryString<Brand>(new SearchByQueryParameters()
            {
                IndexName = "brands",
                From = request.PageRequest.PageIndex,
                Size = request.PageRequest.PageSize,
                Fields = new[] { "name" },
                Query="Mini 50",
                QueryName="brand_search"
            });

            var brandList = brandsFromElastic.Select(i => i.Item).ToList();

            //List<GetListBrandListItemDto> response = _mapper.Map<List<GetListBrandListItemDto>>(brandList);
            return brandList;
        }
    }
}