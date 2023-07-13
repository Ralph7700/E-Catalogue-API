using AutoMapper;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Helpers;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Products;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class GetAllProductsQuery : IRequest<(List<ReadProductDto>,PaginationMetaData)>
{
    public int PageNumber { get; set; }
    
    public GetAllProductsQuery(int pageNumber)
    {
        PageNumber = pageNumber;
    }
}

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, (List<ReadProductDto>,PaginationMetaData)>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task< (List<ReadProductDto>,PaginationMetaData)> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var (products,paginationMetaData) = await _productRepository.GetAllProducts(request.PageNumber);
        return (_mapper.Map<List<ReadProductDto>>(products),paginationMetaData);
    }
}
