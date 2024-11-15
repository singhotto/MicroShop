using Microsoft.EntityFrameworkCore;
using Supply.Repository.Model;

namespace Supply.Repository;

public class SupplyDbContext : DbContext {
    public SupplyDbContext(DbContextOptions<SupplyDbContext> options) : base(options) { }

    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Order> Order { get; set; }
    public DbSet<Supplier> Supplier { get; set; }
    public DbSet<ProductOrderList> ProductOrdersList { get; set; }
    public DbSet<TransactionalOutbox> TransactionalOutboxList { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Product_Id);
        modelBuilder.Entity<Category>().HasKey(c => c.Category_Id);
        modelBuilder.Entity<Supplier>().HasKey(s => s.User_Id);
        modelBuilder.Entity<Order>().HasKey(o => o.Order_Id);
        modelBuilder.Entity<ProductOrderList>().HasKey(po => po.Product_Order_Id);

        modelBuilder.Entity<TransactionalOutbox>().ToTable("TransactionalOutbox");
        modelBuilder.Entity<TransactionalOutbox>().HasKey(e => new { e.Id });

        modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasPrecision(18, 2);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.Category_Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Supplier)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.User_Id).IsRequired();

        modelBuilder.Entity<ProductOrderList>()
            .HasOne(po => po.Order)
            .WithMany(o=>o.Products)
            .HasForeignKey(po => po.Order_Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<ProductOrderList>()
            .HasOne(po => po.Product)
            .WithMany(p => p.ProductOrderList)
            .HasForeignKey(po => po.Product_Id)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Supplier)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.User_Id)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
