using AutoMapper;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Products;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class UpdateProductCommand : IRequest<ReadProductDto> 
{
    public UpdateProductDto UpdateProductDto { get; set; }

    public UpdateProductCommand(UpdateProductDto updateProductDto)
    {
        UpdateProductDto = updateProductDto;
    }
}

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ReadProductDto>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ReadProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetProductById(request.UpdateProductDto.ProductId);
        _mapper.Map(request.UpdateProductDto, product);
        await _productRepository.UpdateProduct(product);
        var result = await _productRepository.GetProductById(product.ProductId);
        return _mapper.Map<ReadProductDto>(result);
    }
}