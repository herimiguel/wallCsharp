using System.ComponentModel.DataAnnotations;

namespace wall.Models
{
    public class RegisterViewModel : BaseEntity
    {        
        [Required]
        [MinLength(2, ErrorMessage = "Names must be at least 2 characters")]
        [Display(Name = "First Name")]
        public string first_name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Names must be at least 2 characters")]
        [Display(Name = "Last Name")]
        public string last_name { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string email { get; set; }
        
        [Required]
        [MinLength(8, ErrorMessage = "Passwords must be at least 8 characters")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Passwords must be at least 8 characters")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "The password and confirmation do not match.")]
        [Display(Name = "Password Confirmation")]
        public string confirm { get; set; }
    }
}