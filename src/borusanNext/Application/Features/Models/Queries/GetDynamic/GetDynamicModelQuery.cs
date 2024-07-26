using Application.Features.Brands.Queries.GetDynamic;
using Application.Features.Models.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Dynamic;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Models.Queries.GetDynamic;

public class GetDynamicModelQuery : IRequest<GetListResponse<GetDynamicModelResponse>>
{
    public PageRequest PageRequest { get; set; }

    public DynamicQuery DynamicQuery { get; set; }
    public class GetDynamicQueryHandler : IRequestHandler<GetDynamicModelQuery, GetListResponse<GetDynamicModelResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ModelBusinessRules _modelBusinessRules;
        private readonly IModelRepository _modelRepository;

        public GetDynamicQueryHandler(IMapper mapper, ModelBusinessRules modelBusinessRules, IModelRepository modelRepository)
        {
            _mapper = mapper;
            _modelBusinessRules = modelBusinessRules;
            _modelRepository = modelRepository;
        }

        public async Task<GetListResponse<GetDynamicModelResponse>> Handle(GetDynamicModelQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(
             dynamic: request.DynamicQuery,
             index: request.PageRequest.PageIndex,
             include: i=>i.Include(i=>i.Brand),
             size: request.PageRequest.PageSize,
             cancellationToken: cancellationToken);


            GetListResponse<GetDynamicModelResponse> response = _mapper.Map<GetListResponse<GetDynamicModelResponse>>(models);
            return response;
        }
    }
}
