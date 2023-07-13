using AutoMapper;
using e_catalog_backend.Dtos.SubCategory;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.SubCategories;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetSubCategoryByIdQuery : IRequest<ReadSubCategoryDto>
{
    public Guid Id { get; set; }
    public GetSubCategoryByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, ReadSubCategoryDto>
{
    private readonly ISubCategoryRepository _subCategoryRepository;
    private readonly IMapper _mapper;

    public GetSubCategoryByIdQueryHandler(ISubCategoryRepository subCategoryRepository, IMapper mapper)
    {
        _subCategoryRepository = subCategoryRepository;
        _mapper = mapper;
    }

    public async Task<ReadSubCategoryDto> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _subCategoryRepository.GetSubCategoryById(request.Id);

        return _mapper.Map<ReadSubCategoryDto>(result);
    }
}