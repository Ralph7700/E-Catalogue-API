using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Products;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class DeleteProductCommand:IRequest
{
    public Guid ProductId { get; set; }

    public DeleteProductCommand(Guid productId)
    {
        ProductId = productId;
    }
}

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteProduct(request.ProductId);
        return Unit.Value;
    }
}