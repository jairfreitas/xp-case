using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Transaction;

public class CreateTransactionDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O nome não pode ter mais que 100 caracteres.")]
    [RegularExpression("^(Stock|Account)$", ErrorMessage = "O valor deve ser Stock ou Account.")]
    public string Name { get; set; }

    /// <summary>
    ///     O valor deve ser Digital Account, Buy, Withdraw ou Deposit.
    /// </summary>
    [Required(ErrorMessage = "O tipo é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O tipo não pode ter mais que 100 caracteres.")]
    [RegularExpression("^(Sell|Buy|Withdraw|Deposit)$",
        ErrorMessage = "O valor deve ser Sell, Buy, Withdraw ou Deposit.")]
    public string Type { get; set; }

    public Guid CustomerId { get; set; }

    [Required(ErrorMessage = "O valor unitário é obrigatório.")]
    public decimal Price { get; set; }
}