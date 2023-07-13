using AutoMapper;
using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Repositories.Categories;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetAllCategoriesQuery : IRequest<List<ReadCategoryDto>>
{
}

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, List<ReadCategoryDto>>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<List<ReadCategoryDto>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetAllCategories();

        return _mapper.Map<List<ReadCategoryDto>>(result);
    }
}