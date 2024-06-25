using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Account;

public class AccountDto
{
    [Required(ErrorMessage = "O ID da conta é obrigatório.")]
    public Guid AccountId { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [MaxLength(255, ErrorMessage = "O nome não pode ter mais que 255 caracteres.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
    [MaxLength(255, ErrorMessage = "O email não pode ter mais que 255 caracteres.")]
    public string Email { get; set; }

    public DateTime CreatedAt { get; set; }
}