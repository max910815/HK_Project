using System.ComponentModel.DataAnnotations;

namespace HK_project.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "必須輸入")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "必須輸入")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
