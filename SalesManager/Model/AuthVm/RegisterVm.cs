using SalesManager.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SalesManager.Model.ViewModels
{
    public class RegisterVm
    {
        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 6)]
        public string ConfirmPassword { get; set; }

        //[Required]
        [StringLength(15, MinimumLength = 2)]
        public string Branch { get; set; }

        //[Required]
        [DefaultValue(false)]
        public bool RememberMe { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string FullName { get; set; }

        internal ApplicationUser Transform() => new() { UserName = UserName, Password = Password, FullName = FullName };
    }
}
