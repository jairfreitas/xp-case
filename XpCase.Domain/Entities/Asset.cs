using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace XpCase.Domain.Entities
{
    public class Asset
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AssetId { get; set; }

        public string Name { get; set; }

        public string Symbol { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Type { get; set; }
    }
}
