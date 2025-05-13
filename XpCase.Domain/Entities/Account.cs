using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XpCase.Domain.Entities;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid AccountId { get; set; }

    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }
}