using Application.Features.Brands.Constants;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.Brands.Constants.BrandsOperationClaims;
using NArchitecture.Core.Application.Pipelines.Caching;

namespace Application.Features.Brands.Queries.GetById;

public class GetByIdBrandQuery : IRequest<GetByIdBrandResponse>, ICachableRequest
{
    public Guid Id { get; set; }

    public bool BypassCache => false;

    public string CacheKey => $"GetByIdBrandQuery.{Id}";

    public string? CacheGroupKey => "Brand.Get";

    public TimeSpan? SlidingExpiration { get; }

    public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, GetByIdBrandResponse>
    {
        private readonly IMapper _mapper;
        private readonly IBrandRepository _brandRepository;
        private readonly BrandBusinessRules _brandBusinessRules;

        public GetByIdBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository, BrandBusinessRules brandBusinessRules)
        {
            _mapper = mapper;
            _brandRepository = brandRepository;
            _brandBusinessRules = brandBusinessRules;
        }

        public async Task<GetByIdBrandResponse> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
        {
            Brand? brand = await _brandRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);
            await _brandBusinessRules.BrandShouldExistWhenSelected(brand);

            GetByIdBrandResponse response = _mapper.Map<GetByIdBrandResponse>(brand);
            return response;
        }
    }
}