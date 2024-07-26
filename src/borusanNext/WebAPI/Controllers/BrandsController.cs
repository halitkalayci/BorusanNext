using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;
using Application.Features.Brands.Queries.GetDynamic;
using NArchitecture.Core.Persistence.Dynamic;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
    [HttpGet("Example")]
    public async Task<IActionResult> Example()
    {
        return Ok();
    }


    [HttpPost]
    public async Task<IActionResult> Add([FromForm] CreateBrandCommand createBrandCommand)
    {
        var x = HttpContext.User;
        CreatedBrandResponse response = await Mediator.Send(createBrandCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateBrandCommand)
    {
        UpdatedBrandResponse response = await Mediator.Send(updateBrandCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBrandResponse response = await Mediator.Send(new DeleteBrandCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBrandResponse response = await Mediator.Send(new GetByIdBrandQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<ActionResult<GetListResponse<GetListBrandListItemDto>>> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBrandQuery getListBrandQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBrandListItemDto> response = await Mediator.Send(getListBrandQuery);
        return Ok(response);
    }
    
    [HttpPost("GetDynamic")]
    public async Task<IActionResult> GetDynamic([FromBody] DynamicQuery dynamicQuery, [FromQuery] int pageSize, [FromQuery] int pageIndex)
    {
        GetDynamicQuery getDynamicQuery = new()
        {
            DynamicQuery = dynamicQuery,
            PageRequest = new() { PageIndex = pageIndex, PageSize = pageSize }
        };
        var response = await Mediator.Send(getDynamicQuery);
        return Ok(response);
    }
}
