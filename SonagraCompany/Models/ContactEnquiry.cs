using System.ComponentModel.DataAnnotations;

namespace SonagraCompany.Models
{
    public class ContactEnquiry
    {
        [Required(ErrorMessage = "Please enter your name."), StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email."), EmailAddress, StringLength(150)]
        public string Email { get; set; } = string.Empty;

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must contain exactly 10 digits.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please tell us about your requirement."), StringLength(3000)]
        public string Message { get; set; } = string.Empty;
    }
}
