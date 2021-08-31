using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManager.Model
{
    public class Prices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PricesID { get; set; }

        [Required]
        [Range(1, double.MaxValue)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required]
        public int ItemsID { get; set; }

        public DateTime DateSet { get; set; }

        [StringLength(30, MinimumLength = 3)]
        public string Setter { get; set; }

        [Timestamp, ConcurrencyCheck]
        public byte[] Concurrency { get; set; }

        public virtual Items Items { get; set; }
    }
}
