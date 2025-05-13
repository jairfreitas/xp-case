using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Order;

public class OrderDto
{
    [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
    public Guid OrderId { get; set; }

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

    [Required(ErrorMessage = "O tipo é obrigatório.")]
    public bool IsBuyOrder { get; set; }

    /// <summary>
    ///     O valor deve ser Approved, Rejected, Pending ou Partial.
    /// </summary>
    [RegularExpression("^(Approved|Rejected|Pending|Partial)$",
        ErrorMessage = "O valor deve ser Approved, Rejected, Pending ou Partial.")]
    [Required(ErrorMessage = "O status é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O status não pode ter mais que 100 caracteres.")]
    public string Status { get; set; }

    public DateTime CreatedAt { get; set; }
}