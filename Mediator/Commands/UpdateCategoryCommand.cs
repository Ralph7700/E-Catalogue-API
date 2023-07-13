using AutoMapper;
using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Models;
using e_catalog_backend.Repositories.Categories;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;

public class UpdateCategoryCommand : IRequest<ReadCategoryDto>
{
    public UpdateCategoryDto Category { get; set; }
    
    public UpdateCategoryCommand(UpdateCategoryDto category)
    {
        Category = category;
    }
}

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ReadCategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ReadCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request.Category);

        var result = await _categoryRepository.UpdateCategory(category);

        return _mapper.Map<ReadCategoryDto>(result);
    }
}