using System.ComponentModel.DataAnnotations;

namespace SalesManager.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
