using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Asset;

public class CreateAssetDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(255, ErrorMessage = "O nome não pode ter mais que 255 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O código é obrigatório.")]
    [MaxLength(50, ErrorMessage = "O código não pode ter mais que 50 caracteres.")]
    public string Symbol { get; set; }

    [Required(ErrorMessage = "O valor unitário é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O valor unitário deve ser maior ou igual a zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(0, int.MaxValue, ErrorMessage = "A quantidade deve ser maior ou igual a zero.")]
    public int Quantity { get; set; }

    public DateTime ExpirationDate { get; set; }

    /// <summary>
    ///     O valor deve ser Ações, Tesouro Direto ou Fundos de Investimento.
    /// </summary>
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O tipo não pode ter mais que 100 caracteres.")]
    [RegularExpression("^(Ações|Tesouro Direto|Fundos de Investimento)$",
        ErrorMessage = "O valor deve ser Ações, Tesouro Direto ou Fundos de Investimento.")]
    public string Type { get; set; }
}