using System;
using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Asset
{
    public class AssetDto
    {
        public Guid AssetId { get; set; }

        [Required(ErrorMessage = "O nome do ativo é obrigatório.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O código do ativo é obrigatório.")]
        public string Symbol { get; set; }

        [Required(ErrorMessage = "O valor unitário do ativo é obrigatório.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "A quantidade do ativo é obrigatória.")]
        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// O valor deve ser Ações, Tesouro Direto ou Fundos de Investimento.
        /// </summary>
        [Required(ErrorMessage = "O tipo é obrigatório.")]
        [MaxLength(100, ErrorMessage = "O tipo não pode ter mais que 100 caracteres.")]
        [RegularExpression("^(Ações|Tesouro Direto|Fundos de Investimento)$", ErrorMessage = "O valor deve ser Ações, Tesouro Direto ou Fundos de Investimento.")]
        public string Type { get; set; }
    }
}
