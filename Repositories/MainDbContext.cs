using Microsoft.EntityFrameworkCore;

namespace e_catalog_backend.Repositories;
public class MainDbContext : DbContext
{
    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options)
    {
    }

    public DbSet<Models.Product> Products { get; set; }
    public DbSet<Models.Category> Categories { get; set; }
    public DbSet<Models.User> Users { get; set; }
    public DbSet<Models.Order> Orders { get; set; }
    public DbSet<Models.OrderItem> OrderItems { get; set; }
    public DbSet<Models.SubCategory> SubCategories { get; set; }
    public DbSet<Models.ProductImage> ProductImages { get; set; }

}