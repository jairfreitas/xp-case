using System;
using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Wallet
{
    public class CreateWalletDto
    {
        [Required(ErrorMessage = "AssetId é obrigatório.")]
        public Guid AssetId { get; set; }

        [Required(ErrorMessage = "CustomerId é obrigatório.")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "Quantity é obrigatório.")]
        public int Quantity { get; set; }
    }
}
