using Microsoft.EntityFrameworkCore;
using XpCase.Domain.Entities;

namespace XpCase.Infrastructure.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                new Account
                {
                    AccountId = Guid.NewGuid(),
                    Name = "João da Silva",
                    Email = "joao@operacao.com",
                    CreatedAt = DateTime.Now
                },
                new Account
                {
                    AccountId = Guid.NewGuid(),
                    Name = "Maria da Silva",
                    Email = "maria@operacao.com",
                    CreatedAt = DateTime.Now
                },
                new Account
                {
                    AccountId = Guid.NewGuid(),
                    Name = "José da Silva",
                    Email = "jose@operacao.com",
                    CreatedAt = DateTime.Now
                }
            );

            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    AssetId = Guid.NewGuid(),
                    Name = "Ação XYZ",
                    Symbol = "ACXYZ",
                    Price = 100m,
                    Quantity = 100,
                    ExpirationDate = DateTime.Now.AddYears(5),
                    CreatedAt = DateTime.Now,
                    Type = "Ações"
                },
                new Asset
                {
                    AssetId = Guid.NewGuid(),
                    Name = "Tesouro Selic 2025",
                    Symbol = "TS2025",
                    Price = 50m,
                    Quantity = 200,
                    ExpirationDate = DateTime.Now.AddYears(3),
                    CreatedAt = DateTime.Now,
                    Type = "Tesouro Direto"
                },
                new Asset
                {
                    AssetId = Guid.NewGuid(),
                    Name = "Fundo Imobiliário ABC",
                    Symbol = "FIABC",
                    Price = 150.00m,
                    Quantity = 50,
                    ExpirationDate = DateTime.Now.AddYears(3),
                    CreatedAt = DateTime.Now,
                    Type = "Fundos de Investimentos"
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    Name = "Ricardo dos Santos",
                    Email = "ricardo@customer.com",
                    Amount = 5000.00m,
                    CreatedAt = DateTime.Now
                },
                new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    Name = "Ana dos Santos",
                    Email = "ana@customer.com",
                    Amount = 10000.00m,
                    CreatedAt = DateTime.Now
                },
                new Customer
                {
                    CustomerId = Guid.NewGuid(),
                    Name = "Felipe dos Santos",
                    Email = "felipe@customer.com",
                    Amount = 300.00m,
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
