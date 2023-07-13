using e_catalog_backend.Dtos.Order;
using e_catalog_backend.Mediator.Commands;
using e_catalog_backend.Mediator.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Guid = System.Guid;

namespace e_catalog_backend.Controllers;

[ApiController]
[Route("api/v1/orders")]
[Authorize(Policy ="Salesman")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Route("create")]
    [HttpPost]
    public async Task<ActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            createOrderDto.UserId = Guid.Parse(userId!);
            var result = await _mediator.Send(new CreateOrderCommand(createOrderDto));
            return Created(nameof(GetOrderById), new {id = result});
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("get-order-by-id/{id}")]
    [HttpGet]
    public async Task<ActionResult> GetOrderById([FromQuery]Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetOrderByIdQuery(id));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [Route("get-orders-by-user-id")]
    [HttpGet]
    public async Task<ActionResult> GetOrdersByUserId()
    {
        try
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            var result = await _mediator.Send(new GetOrdersByUserIdQuery(Guid.Parse(userId!)));
            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}