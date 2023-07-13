using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Mediator.Commands;
using e_catalog_backend.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_catalog_backend.Controllers;

[ApiController]
[Route("api/v1/categories")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Route("create")]
    [Authorize(Policy ="Manager")]
    [HttpPost]
    public async Task<ActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        try
        {
            var result = await _mediator.Send(new CreateCategoryCommand(createCategoryDto));
            return Created(nameof(GetCategoryById), new {id = result});
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("update")]
    [Authorize(Policy ="Manager")]
    [HttpPut]
    public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
    {
        try
        {
            var result = await _mediator.Send(new UpdateCategoryCommand(updateCategoryDto));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("delete")]
    [Authorize(Policy ="Manager")]
    [HttpDelete]
    public async Task<ActionResult> DeleteCategory([FromBody] Guid categoryId)
    {
        try
        {
            await _mediator.Send(new DeleteCategoryCommand(categoryId));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("get-all")]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetAllCategories()
    {
        try
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Route("get-by-id/{id}")]
    [Authorize]
    [HttpGet]
    public async Task<ActionResult> GetCategoryById([FromQuery] Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery(id));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}