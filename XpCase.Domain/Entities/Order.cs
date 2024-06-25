using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XpCase.Domain.Entities
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid OrderId { get; set; }
        public Guid AssetId { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsBuyOrder { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(AssetId))]
        public Asset Asset { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public Customer Customer { get; set; }
    }
}
