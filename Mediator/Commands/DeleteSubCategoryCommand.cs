using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.SubCategories;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class DeleteSubCategoryCommand : IRequest
{
    public Guid Id { get; set; }
    
    public DeleteSubCategoryCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand>
{
    private readonly ISubCategoryRepository _subCategoryRepository;

    public DeleteSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository)
    {
        _subCategoryRepository = subCategoryRepository;
    }

    public async Task<Unit> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
    {
        await _subCategoryRepository.DeleteSubCategory(request.Id);

        return Unit.Value;
    }
}