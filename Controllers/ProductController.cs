using System.Security.Claims;
using System.Text.Json;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Mediator.Commands;
using e_catalog_backend.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_catalog_backend.Controllers;

[ApiController]
[Route("api/v1/products")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("create")]
    [HttpPost]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
    {
        try
        {
            var result =
                await _mediator.Send(new CreateProductCommand(createProductDto,
                    User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return Created(nameof(GetProductById), new {id = result});
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("update")]
    [HttpPut]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductDto updateProductDto)
    {
        try
        {
            
                var result =await _mediator.Send(new UpdateProductCommand(updateProductDto));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("delete")]
    [HttpDelete]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> DeleteProduct([FromBody] Guid productId)
    {
        try
        {
            var result =
                await _mediator.Send(new DeleteProductCommand(productId));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("get-all")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetAllProducts([FromQuery]int pageNumber = 1)
    {
        try
        {
            var (result,pagingMetaData) = await _mediator.Send(new GetAllProductsQuery(pageNumber));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("get-by-id/{productId}")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetProductById([FromQuery] Guid productId)
    {
        try
        {
            var result = await _mediator.Send(new GetProductByIdQuery(productId));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("search-product")]
    [HttpGet]
    [Authorize(Policy ="Salesman")]
    public async Task<ActionResult> SearchProduct(
        [FromQuery] string? searchQuery,
        [FromQuery] string? category,
        [FromQuery] string? subcategory,
        [FromQuery] double? maximumPrice,
        [FromQuery] double minimumPrice = 0,
        [FromQuery] int pageNumber = 1)
    {
        var query = new SearchProductQuery
        (
             searchQuery,
             category,
             subcategory,
             maximumPrice,
             minimumPrice,
             pageNumber
        );

        var (result,pagingMetaData) = await _mediator.Send(query);
        Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagingMetaData));
        return Ok(result);
    }
}
    
    



 