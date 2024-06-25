using Microsoft.EntityFrameworkCore;
using XpCase.Domain.Entities;

namespace XpCase.Infrastructure.Data
{
    public class XpCaseDbContext : DbContext
    {
        public XpCaseDbContext(DbContextOptions<XpCaseDbContext> options) : base(options) { }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Asset?> Assets { get; set; }
        public DbSet<Customer?> Customers { get; set; }
        public DbSet<Order?> Orders { get; set; }
        public DbSet<Wallet?> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("accounts");
                entity.HasKey(e => e.AccountId);
                entity.Property(e => e.AccountId)
                    .HasColumnName("account_id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.HasIndex(e => e.Email)
                    .IsUnique();
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Asset>(entity =>
            {
                entity.ToTable("assets");
                entity.HasKey(e => e.AssetId);
                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Symbol)
                    .HasColumnName("symbol")
                    .IsRequired()
                    .HasMaxLength(50);
                entity.HasIndex(e => e.Symbol)
                    .IsUnique();
                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .IsRequired();
                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity")
                    .IsRequired();
                entity.Property(e => e.ExpirationDate)
                    .HasColumnName("expiration_date")
                    .IsRequired();
  entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Type)
                    .HasColumnName("types")
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("customers");
                entity.HasKey(e => e.CustomerId);
                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETDATE()");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("orders");
                entity.HasKey(e => e.OrderId);
                entity.Property(e => e.OrderId)
                    .HasColumnName("order_id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id");
                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id");
                entity.Property(e => e.Price)
                    .HasColumnName("price");
                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity");
                entity.Property(e => e.IsBuyOrder)
                    .HasColumnName("is_buy_order")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Asset)
                    .WithMany()
                    .HasForeignKey(e => e.AssetId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.ToTable("wallets");
                entity.HasKey(e => e.WalletId);
                entity.Property(e => e.WalletId)
                    .HasColumnName("wallet_id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.AssetId)
                    .HasColumnName("asset_id");
                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id");
                entity.Property(e => e.Quantity)
                    .HasColumnName("quantity");
                entity.Property(e => e.UpdatedAt)
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasIndex(w => new { w.AssetId, w.CustomerId }).IsUnique();

                entity.HasOne(e => e.Asset)
                    .WithMany()
                    .HasForeignKey(e => e.AssetId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("transactions");
                entity.HasKey(e => e.TransactionId);
                entity.Property(e => e.TransactionId)
                    .HasColumnName("transaction_id")
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .IsRequired()
                    .HasMaxLength(255);
                entity.Property(e => e.CustomerId)
                    .HasColumnName("customer_id");
                entity.Property(e => e.Price)
                    .HasColumnName("price");
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("GETDATE()");

                entity.HasOne(e => e.Customer)
                    .WithMany()
                    .HasForeignKey(e => e.CustomerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Seed();
        }
    }
}
