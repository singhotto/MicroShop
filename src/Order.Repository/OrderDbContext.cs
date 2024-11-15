using Microsoft.EntityFrameworkCore;
using Order.Repository.Model;

namespace Order.Repository;

public class OrderDbContext : DbContext {
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }

    public DbSet<Product> Product { get; set; }
    public DbSet<Orders> Order { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<OrderProductList> OrderProductList { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>().HasKey(p => p.Product_Id);
        modelBuilder.Entity<Orders>().HasKey(o => o.Order_Id);
        modelBuilder.Entity<User>().HasKey(u => u.User_Id);
        modelBuilder.Entity<OrderProductList>().HasKey(po => po.Order_Product_Id);

        modelBuilder.Entity<Product>()
        .Property(p => p.Price)
        .HasPrecision(18, 2);

        modelBuilder.Entity<Orders>()
        .Property(o => o.Amount)
        .HasPrecision(18, 2);

        modelBuilder.Entity<Orders>()
            .HasOne( o => o.User)
            .WithMany( u => u.Orders)
            .HasForeignKey( o => o.User_Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<OrderProductList>()
            .HasOne(po => po.Order)
            .WithMany(o=>o.Products)
            .HasForeignKey(po => po.Order_Id)
            .HasForeignKey(po => po.Product_Id)
            .OnDelete(DeleteBehavior.NoAction);


        modelBuilder.Entity<OrderProductList>()
            .HasOne(op => op.Order)
            .WithMany(o => o.Products)
            .HasForeignKey(op => op.Order_Id);

        modelBuilder.Entity<OrderProductList>()
            .HasOne(op => op.Product)
            .WithMany(p => p.Orders)
            .HasForeignKey(op => op.Product_Id);

        modelBuilder.Entity<User>()
            .HasData(new User()
            {
                User_Id = "admin@micro.it",
                FirstName = "Otto",
                LastName = "Killer",
                Address = "Via Roma 20",
            });
    }
}
