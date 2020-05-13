using System.ComponentModel.DataAnnotations;

namespace DaHo.M151.DataFormatValidator.Models.ApiModels
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
