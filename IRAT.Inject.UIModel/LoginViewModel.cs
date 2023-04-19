using System.ComponentModel.DataAnnotations;

namespace IRAT.Inject.UIModel
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User name")]
        public string Username
        {
            get;
            set;
        }

        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        [Required]
        public string Password
        {
            get;
            set;
        }

        [Display(Name = "Remember me?")]
        public bool RememberMe
        {
            get;
            set;
        }

        //[Required]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email
        {
            get;
            set;
        }

    }
}
