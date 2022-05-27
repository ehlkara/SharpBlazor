using System.ComponentModel.DataAnnotations;

namespace Sharp_Models
{
    public class SignInRequestDto
    {
        [Required(ErrorMessage = "UserName is required")]
        [RegularExpression("^[a-zA-z0-9_.-]+@[a-zA-z0-9-]+.[a-zA-z0-9-.]+$",ErrorMessage = "Invalid username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
