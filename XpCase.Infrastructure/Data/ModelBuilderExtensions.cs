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
                    AccountId = new Guid("88b2a625-41b7-4a5f-a184-84dbad26cc7d"),
                    Name = "João da Silva",
                    Email = "joao@operacao.com",
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Account
                {
                    AccountId = new Guid("8362097d-fa43-479e-83a5-f8a91f09f2ec"),
                    Name = "Maria da Silva",
                    Email = "maria@operacao.com",
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Account
                {
                    AccountId = new Guid("2ffae86a-d19c-419a-a02f-5fc992b89ddd"),
                    Name = "José da Silva",
                    Email = "jose@operacao.com",
                    CreatedAt = new DateTime(2025, 1, 1)
                }
            );

            modelBuilder.Entity<Asset>().HasData(
                new Asset
                {
                    AssetId = new Guid("f1584129-1b96-4e0e-a46c-9459260e0d2e"),
                    Name = "Ação XYZ",
                    Symbol = "ACXYZ",
                    Price = 100m,
                    Quantity = 100,
                    ExpirationDate = new DateTime(2030, 1, 1),
                    CreatedAt = new DateTime(2025, 1, 1),
                    Type = "Ações"
                },
                new Asset
                {
                    AssetId = new Guid("b102b62f-aacb-4c10-a839-806699de8781"),
                    Name = "Tesouro Selic 2025",
                    Symbol = "TS2025",
                    Price = 50m,
                    Quantity = 200,
                    ExpirationDate = new DateTime(2030, 1, 1),
                    CreatedAt = new DateTime(2025, 1, 1),
                    Type = "Tesouro Direto"
                },
                new Asset
                {
                    AssetId = new Guid("f53328fe-b322-4182-8cc3-8d883c8e6a78"),
                    Name = "Fundo Imobiliário ABC",
                    Symbol = "FIABC",
                    Price = 150.00m,
                    Quantity = 50,
                    ExpirationDate = new DateTime(2030, 1, 1),
                    CreatedAt = new DateTime(2025, 1, 1),
                    Type = "Fundos de Investimentos"
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = new Guid("118afd34-cb8b-4039-b76f-148759e47374"),
                    Name = "Ricardo dos Santos",
                    Email = "ricardo@customer.com",
                    Amount = 5000.00m,
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Customer
                {
                    CustomerId = new Guid("13e5ce20-9ddc-4502-9527-9a9be0b94c45"),
                    Name = "Ana dos Santos",
                    Email = "ana@customer.com",
                    Amount = 10000.00m,
                    CreatedAt = new DateTime(2025, 1, 1)
                },
                new Customer
                {
                    CustomerId = new Guid("1934592b-0942-4876-9d57-8253c0c30fec"),
                    Name = "Felipe dos Santos",
                    Email = "felipe@customer.com",
                    Amount = 300.00m,
                    CreatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}
