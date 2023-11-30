using System.ComponentModel.DataAnnotations;

namespace EAMS.AuthViewModels
{
    public class ValidateMobileViewModel
    {
        [Required(ErrorMessage ="Please Enter Mobile Number")]
        public string MobileNumber { get; set; }
        public string Otp { get; set; }
    }
}
