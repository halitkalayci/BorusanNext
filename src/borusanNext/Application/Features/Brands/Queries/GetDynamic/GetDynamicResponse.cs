using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Brands.Queries.GetDynamic;

public class GetDynamicResponse : IDto {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
}
