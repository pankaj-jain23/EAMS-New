using System.ComponentModel.DataAnnotations;

namespace EAMS.AuthViewModels
{
    public class ValidateMobileViewModel
    {
        [Phone]
        [Required(ErrorMessage ="Please Enter Mobile Number")]
        public string MobileNumber { get; set; }
        public int Otp { get; set; }
    }
}
