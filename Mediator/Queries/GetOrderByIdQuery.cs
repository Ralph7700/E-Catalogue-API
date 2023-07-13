using AutoMapper;
using e_catalog_backend.Dtos.Order;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Orders;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetOrderByIdQuery : IRequest<ReadOrderDto>
{
    public Guid OrderId { get; set; }
    
    public GetOrderByIdQuery(Guid orderId)
    {
        OrderId = orderId;
    }
}

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ReadOrderDto>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<ReadOrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetOrderById(request.OrderId);
        return _mapper.Map<ReadOrderDto>(order);
    }
}