using System.ComponentModel.DataAnnotations;

namespace EAMS.AuthViewModels
{
    public class GetRefreshTokenViewModel
    {
        [Required]
        public string AccessToken { get; set; }
        [Required]
        public string RefreshToken { get; set; }
    }
}
