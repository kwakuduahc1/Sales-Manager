using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManager.Model
{
    public class PaymentTypes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte PaymentTypesID { get; set; }

        [StringLength(15, MinimumLength = 4)]
        [Required]
        public string PaymentType { get; set; }

        public virtual ICollection<SupplierPayments> SupplierPayments { get; set; }
    }
}
