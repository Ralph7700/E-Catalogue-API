using System.Security.Claims;
using e_catalog_backend.Dtos.User;
using e_catalog_backend.Mediator.Commands;
using e_catalog_backend.Mediator.Queries;
using e_catalog_backend.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_catalog_backend.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Route("create-manager")]
    [HttpPost]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> CreateManager([FromForm]CreateUserDto createUserDto)
    {
        try {
            var result = await _mediator.Send(new CreateUserCommand(createUserDto, Role.Manager));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("update-manager")]
    [HttpPut]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> UpdateManager([FromBody]UpdateUserDto updateManagerDto)
    {
        try {
            var targetId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mediator.Send(new UpdateUserCommand(updateManagerDto, targetId));
            return Ok();
        }
        catch(Exception e)
        {
            return Forbid(e.Message);
        }
    }
    
    [Route("delete-manager")]
    [HttpDelete]
    [Authorize(Policy ="Manager")]
    public async Task<ActionResult> DeleteManager()
    {
        try
        {
            var targetId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mediator.Send(new DeleteUserCommand(targetId));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("create-salesman")]
    [HttpPost]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult> CreateSalesman([FromForm]CreateUserDto createUserDto)
    {
        try{
            var result = await _mediator.Send(new CreateUserCommand(createUserDto,Role.Salesman));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("update-salesman")]
    [HttpPut]
    [Authorize(Policy = "Salesman")]
    public async Task<ActionResult> UpdateSalesman([FromBody]UpdateUserDto updateSalesmanDto)
    {
        try{
            var targetId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _mediator.Send(new UpdateUserCommand(updateSalesmanDto,targetId));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("delete-salesman")]
    [HttpDelete]
    [Authorize("Manager")]
    public async Task<ActionResult> DeleteSalesman([FromQuery] string id)
    {
        try
        {
            await _mediator.Send(new DeleteUserCommand(id));
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("get-all-salesmen")]
    [HttpGet]
    [Authorize(Policy = "Manager")]
    public async Task<ActionResult> GetAllSalesmen()
    {
        try
        {
            var result = await _mediator.Send(new GetAllSalesmenQuery());
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

}