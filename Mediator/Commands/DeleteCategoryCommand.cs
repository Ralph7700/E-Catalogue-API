using e_catalog_backend.Repositories.Categories;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class DeleteCategoryCommand : IRequest
{
    public Guid CategoryId { get; set; }
    
    public DeleteCategoryCommand(Guid categoryId)
    {
        CategoryId = categoryId;
    }
}

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteCategory(request.CategoryId);

        return Unit.Value;
    }
}