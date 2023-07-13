using AutoMapper;
using e_catalog_backend.Dtos.Product;
using e_catalog_backend.Helpers;
using e_catalog_backend.Repositories;
using e_catalog_backend.Repositories.Products;
using MediatR;

namespace e_catalog_backend.Mediator.Queries;

public class SearchProductQuery : IRequest<(List<ReadProductDto>,PaginationMetaData)>
{
    public string? SearchQuery;
    public string? Category;
    public string? Subcategory;
    public double? MaximumPrice;
    public double MinimumPrice;
    public int PageNumber;
    
    public SearchProductQuery(string? searchQuery, string? category, string? subcategory, double? maximumPrice, double minimumPrice, int pageNumber)
    {
        SearchQuery = searchQuery;
        Category = category;
        Subcategory = subcategory;
        MaximumPrice = maximumPrice;
        MinimumPrice = minimumPrice;
        PageNumber = pageNumber;
    }
}

public class SearchProductQueryHandler : IRequestHandler<SearchProductQuery, (List<ReadProductDto>,PaginationMetaData)>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public SearchProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<(List<ReadProductDto>,PaginationMetaData)> Handle(SearchProductQuery request, CancellationToken cancellationToken)
    {
        var (products,paginationMetaData) = await _productRepository.SearchProducts(request.SearchQuery, request.Category, request.Subcategory, request.MaximumPrice, request.MinimumPrice, request.PageNumber);
        return (_mapper.Map<List<ReadProductDto>>(products),paginationMetaData);
    }
}