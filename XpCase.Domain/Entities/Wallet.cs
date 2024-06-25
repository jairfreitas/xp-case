using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XpCase.Domain.Entities
{
    public class Wallet
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid WalletId { get; set; }
        public Guid AssetId { get; set; }
        public Guid CustomerId { get; set; }
        public int Quantity { get; set; }
        public DateTime UpdatedAt { get; set; }

        [ForeignKey(nameof(AssetId))]
        public Asset Asset { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
