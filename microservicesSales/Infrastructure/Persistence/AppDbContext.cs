using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("PRODUCTS");
            entity.HasKey(e => e.Id);
            
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Price).HasPrecision(18, 2);
            entity.Property(e => e.Stock).IsRequired();

            entity.Property(e => e.RowVersion)
                  .IsRowVersion();
 
        });


        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("SALES");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Date).IsRequired();
            entity.HasMany(typeof(SaleItem), "Items")
                  .WithOne()
                  .HasForeignKey("SaleId")
                  .OnDelete(DeleteBehavior.Cascade);    
        });


        modelBuilder.Entity<SaleItem>(entity =>
        {
            entity.ToTable("SALEITEMS");
            entity.HasKey(e => e.Id);
            entity.Property<Guid>("SaleId");
            entity.Property(e => e.ProductName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Quantity).IsRequired();
            entity.Property(e => e.UnitPrice).HasPrecision(18, 2);
        });
    }
}