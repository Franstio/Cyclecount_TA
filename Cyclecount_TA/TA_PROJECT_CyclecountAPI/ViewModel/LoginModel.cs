using System.ComponentModel.DataAnnotations;

namespace TA_PROJECT_CyclecountAPI.ViewModel
{
    public class LoginModel
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
