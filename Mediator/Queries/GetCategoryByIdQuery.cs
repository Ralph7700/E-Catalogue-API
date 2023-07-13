using AutoMapper;
using e_catalog_backend.Dtos.Category;
using e_catalog_backend.Repositories.Categories;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetCategoryByIdQuery : IRequest<ReadCategoryDto>
{
    public Guid Id { get; set; }
    public GetCategoryByIdQuery(Guid id)
    {
        Id = id;
    }
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ReadCategoryDto>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public async Task<ReadCategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _categoryRepository.GetCategoryById(request.Id);

        return _mapper.Map<ReadCategoryDto>(result);
    }
}