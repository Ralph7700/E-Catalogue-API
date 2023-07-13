using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Images;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class DeleteProductImageCommand : IRequest
{
    public Guid ProductImageId { get; set; }
    
    public DeleteProductImageCommand(Guid productImageId)
    {
        ProductImageId = productImageId;
    }
}

public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand>
{
    private readonly IImageRepository _productImageRepository;

    public DeleteProductImageCommandHandler(IImageRepository productImageRepository)
    {
        _productImageRepository = productImageRepository;
    }

    public async Task<Unit> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        await _productImageRepository.DeleteProductImage(request.ProductImageId);
        return Unit.Value;
    }
}