using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Order;

public class CreateOrderDto
{
    [Required(ErrorMessage = "O ID do ativo é obrigatório.")]
    public Guid AssetId { get; set; }

    [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "O valor unitário é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "O valor unitário deve ser maior ou igual a zero.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "A quantidade é obrigatória.")]
    [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
    public int Quantity { get; set; }
}