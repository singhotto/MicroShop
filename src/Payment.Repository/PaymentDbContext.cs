using Microsoft.EntityFrameworkCore;
using Payment.Repository.Model;

namespace Payment.Repository;

public class PaymentDbContext : DbContext {
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

    public DbSet<Transaction> Transaction { get; set; }
    public DbSet<TransactionalOutbox> TransactionalOutboxList { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>().HasKey(p => p.Payment_Id);
        modelBuilder.Entity<TransactionalOutbox>().ToTable("TransactionalOutbox");
        modelBuilder.Entity<TransactionalOutbox>().HasKey(e => new { e.Id });

        modelBuilder.Entity<Transaction>()
        .Property(p => p.Amount)
        .HasPrecision(18, 2);
    }
}
