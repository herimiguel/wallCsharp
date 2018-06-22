using System.ComponentModel.DataAnnotations;

namespace wall.Models
{
    public class LoginViewModel : BaseEntity
    {        
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }
        
        [Required]
        [MinLength(8, ErrorMessage = "Passwords must be at least 8 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }
    }
}