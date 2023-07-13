using AutoMapper;
using e_catalog_backend.Dtos.SubCategory;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.SubCategories;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetAllSubCategoriesQuery : IRequest<List<ReadSubCategoryDto>>
{
}

public class GetAllSubcategoriesQueryHandler : IRequestHandler<GetAllSubCategoriesQuery, List<ReadSubCategoryDto>>
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IMapper _mapper;

    public GetAllSubcategoriesQueryHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
    {
        _subCategoryRepository = subCategoryRepository;
        _mapper = mapper;
    }

    public async Task<List<ReadSubCategoryDto>> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var result = await _subCategoryRepository.GetAllSubCategories();

        return _mapper.Map<List<ReadSubCategoryDto>>(result);
    }
}