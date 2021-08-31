using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManager.Model
{
    public class Stockings
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long StockingsID { get; set; }

        [Required]
        public int ItemsID { get; set; }

        public DateTime DateAdded { get; set; }

        [Required]
        public DateTime DateBought { get; set; }

        [Required]
        public short Quantity { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Receipt { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 5)]
        public string Source { get; set; }

        [Range(1, double.MaxValue)]
        [Required]
        public double UnitCost { get; set; }

        [StringLength(30, MinimumLength = 5)]
        public string UserName { get; set; }

        [Timestamp, ConcurrencyCheck]
        public byte[] Concurrency { get; set; }

        public virtual Items Items { get; set; }

    }
}
