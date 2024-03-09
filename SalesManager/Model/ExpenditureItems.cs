using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace SalesManager.Model
{
    public class ExpenditureItems
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenditureItemsID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Item { get; set; }
    }

    public class Expenditure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExpenditureID { get; set; }

        [Required]
        public int ExpenditureItemsID { get; set; }

        [Column(TypeName = "money")]
        [Range(0.1, float.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Receiver { get; set; }

        [NotNull]
        public DateTime DateAdded { get; set; }
    }
}
