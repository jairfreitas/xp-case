using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XpCase.Domain.Entities;

public class Customer
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid CustomerId { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedAt { get; set; }
}