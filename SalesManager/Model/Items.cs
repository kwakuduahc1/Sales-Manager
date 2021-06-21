using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManager.Model
{
    public class Items
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemsID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ItemName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Group { get; set; }

        [Required]
        [DefaultValue(10)]
        public int MinimumStock { get; set; }

        public DateTime DateAdded { get; set; }

        [Timestamp, ConcurrencyCheck]
        public byte[] Concurrency { get; set; }

        public virtual ICollection<Prices> Prices { get; set; }

        public virtual ICollection<Sales> Sales { get; set; }

        public virtual ICollection<Stockings> Stockings { get; set; }
    }
}
