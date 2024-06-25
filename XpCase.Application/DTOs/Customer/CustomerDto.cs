using System;
using System.ComponentModel.DataAnnotations;

namespace XpCase.Application.DTOs.Customer
{
    public class CustomerDto
    {
        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public Guid CustomerId { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [MaxLength(255, ErrorMessage = "O nome não pode ter mais que 255 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
        [MaxLength(255, ErrorMessage = "O email não pode ter mais que 255 caracteres.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O valor deve ser maior ou igual a zero.")]
        public decimal Amount { get; set; }
    }
}
