using AutoMapper;
using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Dtos.SubCategory;
using e_catalog_backend.Models;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.SubCategories;
using MediatR;

namespace e_catalog_backend.Mediator.Commands;
public class CreateSubCategoryCommand : IRequest<ReadSubCategoryDto>
{
    public CreateSubCategoryDto SubCategory { get; set; }
    
    public CreateSubCategoryCommand(CreateSubCategoryDto subCategory)
    {
        SubCategory = subCategory;
    }
}

public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, ReadSubCategoryDto>
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IMapper _mapper;
    public CreateSubCategoryCommandHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
    {
        _subCategoryRepository = subCategoryRepository;
        _mapper = mapper;
    }

    public async Task<ReadSubCategoryDto> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var subCategory = _mapper.Map<SubCategory>(request.SubCategory);

        var result = await _subCategoryRepository.CreateSubCategory(subCategory);

        return _mapper.Map<ReadSubCategoryDto>(result);
    }
}