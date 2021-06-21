using System.ComponentModel.DataAnnotations;

namespace SalesManager.Models
{
    public class URoles
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Role { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 30)]
        public string ID { get; set; }
    }
}