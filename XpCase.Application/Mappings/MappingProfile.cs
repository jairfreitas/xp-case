using AutoMapper;
using XpCase.Application.DTOs.Account;
using XpCase.Application.DTOs.Asset;
using XpCase.Application.DTOs.Customer;
using XpCase.Application.DTOs.Order;
using XpCase.Application.DTOs.Transaction;
using XpCase.Application.DTOs.Wallet;
using XpCase.Domain.Entities;

namespace XpCase.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Account, AccountDto>().ReverseMap();

            CreateMap<Asset, AssetDto>().ReverseMap();
            CreateMap<Asset, CreateAssetDto>().ReverseMap();
            CreateMap<AssetDto, CreateAssetDto>().ReverseMap();

            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, CustomerDto>().ReverseMap();

            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<OrderDto, CreateOrderDto>().ReverseMap();

            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<Transaction, CreateTransactionDto>().ReverseMap();
            CreateMap<TransactionDto, CreateTransactionDto>().ReverseMap();

            CreateMap<Wallet, WalletDto>().ReverseMap();
            CreateMap<Wallet, CreateWalletDto>().ReverseMap();
            CreateMap<WalletDto, CreateWalletDto>().ReverseMap();
        }
    }
}
