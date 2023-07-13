using AutoMapper;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Dtos.User;
using e_catalog_backend.Models;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Products;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class CreateProductCommand : IRequest<Guid>
{
    public CreateProductDto CreateProductDto { get; set; }
    public string UserId { get; set; }

    public CreateProductCommand(CreateProductDto createProductDto, string userId)
    {
        CreateProductDto = createProductDto;
        UserId = userId;
    }
}

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IMapper mapper, IProductRepository productRepository)
    {
        _mapper = mapper;
        _productRepository = productRepository;
    }
    
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request.CreateProductDto);
        await _productRepository.CreateProduct(product);
        return product.ProductId;
    }
}
