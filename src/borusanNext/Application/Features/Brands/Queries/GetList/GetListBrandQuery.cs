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

namespace Application.Features.Brands.Queries.GetList;

public class GetListBrandQuery : IRequest<GetListResponse<GetListBrandListItemDto>>, ICachableRequest, IIntervalRequest
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


    public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, GetListResponse<GetListBrandListItemDto>>
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper, ILogger logger)
        {
            _brandRepository = brandRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<GetListResponse<GetListBrandListItemDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Brand> brands = await _brandRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            var x = 10000;
            await Task.Delay(x);

            _logger.Warning("Bu bir deneme mesajýdýr.");

            GetListResponse<GetListBrandListItemDto> response = _mapper.Map<GetListResponse<GetListBrandListItemDto>>(brands);
            return response;
        }
    }
}