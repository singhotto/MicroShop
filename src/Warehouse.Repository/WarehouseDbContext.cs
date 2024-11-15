using Microsoft.EntityFrameworkCore;
using Warehouse.Repository.Model;

namespace Warehouse.Repository;

public class WarehouseDbContext : DbContext {
    public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        // configurazione del modello tramite API fluent
        modelBuilder.Entity<Product>().ToTable("Product");
        modelBuilder.Entity<Product>().HasKey(p => p.Product_Id);

        modelBuilder.Entity<Category>().ToTable("Category");
        modelBuilder.Entity<Category>().HasKey(c => c.Category_Id);

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey("Category_Id").IsRequired();

        modelBuilder.Entity<TransactionalOutbox>().ToTable("TransactionalOutbox");
        modelBuilder.Entity<TransactionalOutbox>().HasKey(e => new { e.Id });
    }

    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<TransactionalOutbox> TransactionalOutboxList { get; set; }
}
