using NArchitecture.Core.Application.Responses;

namespace Application.Features.Models.Queries.GetDynamic;

public class GetDynamicModelResponse : IResponse {
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string BrandName { get; set; }
}
