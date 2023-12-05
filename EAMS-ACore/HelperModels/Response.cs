using EAMS_ACore.HelperModels;

namespace EAMS.Helper
{
    public class Response
    {
        public RequestStatusEnum Status { get; set; }
        public string? Message { get; set; }
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
