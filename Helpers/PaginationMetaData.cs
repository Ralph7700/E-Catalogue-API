namespace e_catalog_backend.Helpers;

public class PaginationMetaData
{
    public int TotalItemsCount { get; set; }
    public int TotalPageCount { get; set; }
    public int CurrentPage { get; set; }
    
    public PaginationMetaData(int totalItemsCount, int pageSize, int currentPage)
    {
        TotalItemsCount = totalItemsCount;
        TotalPageCount = (int) Math.Ceiling(totalItemsCount / (double) pageSize);
        CurrentPage = currentPage;
    }
}