using MicroShop.Repository.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Repository
{
    public class MicroShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<CartItem> Cart { get; set; }
        public DbSet<TransactionalOutbox> TransactionalOutboxList { get; set; }

        public MicroShopDbContext(DbContextOptions<MicroShopDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionalOutbox>().ToTable("TransactionalOutbox");
            modelBuilder.Entity<TransactionalOutbox>().HasKey(e => new { e.Id });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartItem>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CartItem>()
                .HasIndex(e => e.Product_Id)
                .IsUnique();


            modelBuilder.Entity<CartItem>()
                .Property(c => c.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .HasOne(c => c.User)
                .WithMany(u => u.CartItems)
                .HasForeignKey(u => u.Id)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.CartItems)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .IsRequired(); 

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.CartItems)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasData(new ApplicationUser()
                {
                    Id = "3fb56e65-45d5-425c-89a3-fa7129f29d87",
                    FirstName = "Otto",
                    LastName = "Killer",
                    Address = "Via Roma 20",
                    UserName = "admin@micro.it",
                    NormalizedUserName = "ADMIN@MICRO.IT",
                    Email = "admin@micro.it",
                    NormalizedEmail = "ADMIN@MICRO.IT",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEDrxs5cA726kwKtlpro/RYIBXoSxsggXc+fiekOhhtB1o3HFq5P4DzZThlATpgM/RA==",
                    SecurityStamp = "YJXDVNF4MHTFXJRJHIIY7GSN7AXWUTQW",
                    ConcurrencyStamp = "ec461138-2a37-440e-a06c-06143853efa7",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnd = null,
                    LockoutEnabled = true,
                    AccessFailedCount = 0
                });
        }
    }
}
