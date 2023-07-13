using AutoMapper;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Products;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetProductByIdQuery : IRequest<ReadProductDto>
{
    public Guid ProductId { get; set; }
    
    public GetProductByIdQuery(Guid productId)
    {
        ProductId = productId;
    }
}

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ReadProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ReadProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.ProductId);
        return _mapper.Map<ReadProductDto>(product);
    }
}