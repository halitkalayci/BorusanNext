using Application.Features.Models.Commands.Create;
using Application.Features.Models.Commands.Delete;
using Application.Features.Models.Commands.Update;
using Application.Features.Models.Queries.GetById;
using Application.Features.Models.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;
using Application.Features.Models.Queries.GetDynamic;

namespace Application.Features.Models.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Model, CreateModelCommand>().ReverseMap();
        CreateMap<Model, CreatedModelResponse>().ReverseMap();
        CreateMap<Model, UpdateModelCommand>().ReverseMap();
        CreateMap<Model, UpdatedModelResponse>().ReverseMap();
        CreateMap<Model, DeleteModelCommand>().ReverseMap();
        CreateMap<Model, DeletedModelResponse>().ReverseMap();
        CreateMap<Model, GetByIdModelResponse>().ReverseMap();
        CreateMap<Model, GetListModelListItemDto>().ReverseMap();
        CreateMap<IPaginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();

        CreateMap<Model, GetDynamicModelResponse>().ForMember(i=>i.BrandName, opt => opt.MapFrom(x=>x.Brand.Name)).ReverseMap();
        CreateMap<IPaginate<Model>, GetListResponse<GetDynamicModelResponse>>().ReverseMap();
    }
}