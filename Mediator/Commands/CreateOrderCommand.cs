using AutoMapper;
using e_catalog_backend.Dtos.Order;
using e_catalog_backend.Models;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Orders;
using MediatR;
using Guid = System.Guid;

namespace e_catalog_backend.Mediator.Commands;

public class CreateOrderCommand : IRequest<Guid>
{
    public CreateOrderDto CreateOrderDto { get; set; }

    public CreateOrderCommand(CreateOrderDto createOrderDto)
    {
        CreateOrderDto = createOrderDto;
    }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    
    public CreateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = _mapper.Map<Order>(request.CreateOrderDto);
        await _orderRepository.CreateOrder(order);
        return order.OrderId;
    }
}