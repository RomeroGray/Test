using System.ComponentModel.DataAnnotations;

namespace IRAT.Inject.UIModel
{
    public class ChangePassword
    {
        [Display(Name = "First name")]
        [Required]
        public string Firstname { get; set; }

        [Display(Name = "First name")]
        [Required]
        public string Lastname { get; set; }

        public int UserId { get; set; }

        public string Gid { get; set; }

        [Display(Name = "User name")]
        [Required(ErrorMessage = "Please enter username")]
        public string Username { get; set; }


        [Required(ErrorMessage = "Please enter password")]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Password \"{0}\" must have {2} character", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{6,}$", ErrorMessage = "Password must contain: Minimum 8 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character")]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Please enter confirm password")]
        [Compare("Password", ErrorMessage = "Confirm password doesn't match, Type again !")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}
