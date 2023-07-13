using e_catalog_backend.Dtos.SubCategory;
using e_catalog_backend.Mediator.Commands;
using e_catalog_backend.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_catalog_backend.Controllers;

[ApiController]
[Route("api/v1/subcategories")]
public class SubCategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public SubCategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("create")]
    [HttpPost]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> CreateSubCategory([FromBody] CreateSubCategoryDto createSubCategoryDto)
    {
        try
        {
            var result = await _mediator.Send(new CreateSubCategoryCommand(createSubCategoryDto));
            return Created(nameof(GetSubCategoryById), new {id = result});
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("update")]
    [HttpPut]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> UpdateSubCategory([FromBody] UpdateSubCategoryDto updateSubCategoryDto)
    {
        try
        {
            var result = await _mediator.Send(new UpdateSubCategoryCommand(updateSubCategoryDto));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("delete")]
    [HttpDelete]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> DeleteSubCategory([FromBody] Guid subCategoryId)
    {
        try
        {
            await _mediator.Send(new DeleteSubCategoryCommand(subCategoryId));
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
    public async Task<ActionResult> GetAllSubCategories()
    {
        try
        {
            var result = await _mediator.Send(new GetAllSubCategoriesQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [Route("get-by-id/{id}")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult> GetSubCategoryById([FromQuery] Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetSubCategoryByIdQuery(id));
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }
}