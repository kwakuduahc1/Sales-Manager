using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesManager.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(20, MinimumLength = 5)]
        public override string UserName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string FullName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        [NotMapped]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(30, MinimumLength = 6)]
        [NotMapped]
        [Compare(nameof(Password), ErrorMessage = "Both passwords must match")]
        public string ConfirmPassword { get; set; }
    }
}
