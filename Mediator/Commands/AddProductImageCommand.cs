using AutoMapper;
using e_catalog_backend.Dtos.Image;
using e_catalog_backend.Models;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Images;
using e_catalog_backend.Repositories.Products;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class AddProductImageCommand: IRequest<ReadProductImageDto>
{
    public IFormFile Image { get; set; }
    public CreateProductImageDto CreateProductImageDto { get; set; }

    public AddProductImageCommand(IFormFile image, CreateProductImageDto createProductImageDto)
    {
        Image = image;
        CreateProductImageDto = createProductImageDto;
    }
}

public class AddProductImageCommandHandler : IRequestHandler<AddProductImageCommand, ReadProductImageDto>
{
    private readonly IImageRepository _productImageRepository;
    private readonly IMapper _mapper;

    public AddProductImageCommandHandler(IImageRepository productImageRepository, IProductRepository productRepository, IMapper mapper)
    {
        _productImageRepository = productImageRepository;
        _mapper = mapper;
    }

    public async Task<ReadProductImageDto> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
    {
        var productImage = _mapper.Map<ProductImage>(request.CreateProductImageDto);
        productImage.ImageUrl = await _productImageRepository.UploadProductImage(request.Image, productImage.ProductImageId);
        return _mapper.Map<ReadProductImageDto>(productImage);
    }
}