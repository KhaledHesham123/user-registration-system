using System.ComponentModel.DataAnnotations;

namespace User_Registration_System.Features.UserFeatures.CQRS.Commands.DTOs
{
    public class UserRegisterDto
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
    }
}
