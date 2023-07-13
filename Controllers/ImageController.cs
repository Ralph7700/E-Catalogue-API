using System.Security.Claims;
using e_catalog_backend.Dtos.Image;
using e_catalog_backend.Mediator.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_catalog_backend.Controllers;

[ApiController]
[Route("api/v1/product-images")]
public class ImageController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ImageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Route("add")]
    [HttpPost]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> AddProductImage([FromBody]IFormFile image, [FromQuery] CreateProductImageDto createProductImageDto)
    {
        try
        {
            var result = await _mediator.Send(new AddProductImageCommand(image, createProductImageDto));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    

    [Route("delete-product-image/{id}")]
    [Authorize(Policy ="Manager")]
    [HttpDelete]
    public async Task<ActionResult> DeleteProductImage([FromQuery]string id)
    {
        try
        {
            await _mediator.Send(new DeleteProductImageCommand(Guid.Parse(id)));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
}