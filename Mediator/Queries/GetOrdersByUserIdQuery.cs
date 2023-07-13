using AutoMapper;
using e_catalog_backend.Dtos.Order;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Orders;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetOrdersByUserIdQuery: IRequest<List<ReadOrderDto>>
{
    public Guid UserId { get; set; }

    public GetOrdersByUserIdQuery(Guid userId)
    {
        UserId = userId;
    }
}

public class GetOrdersByUserIdQueryHandler: IRequestHandler<GetOrdersByUserIdQuery, List<ReadOrderDto>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOrdersByUserIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<ReadOrderDto>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
    {
        var orders = await _orderRepository.GetOrdersByUserId(request.UserId);
        return _mapper.Map<List<ReadOrderDto>>(orders);
    }
}