using AutoMapper;
using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Models;
using e_catalog_backend.Repositories.Categories;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class CreateCategoryCommand : IRequest<ReadCategoryDto>
{
    public CreateCategoryDto Category { get; set; }
    
    public CreateCategoryCommand(CreateCategoryDto category)
    {
        Category = category;
    }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ReadCategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ReadCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.Category);

        var result = await _categoryRepository.CreateCategory(category);

        return _mapper.Map<ReadCategoryDto>(result);
    }
}