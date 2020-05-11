using System.ComponentModel.DataAnnotations;

namespace DaHo.M151.DataFormatValidator.Models.ApiModels
{
    public class ValidateFormatRequest
    {
        [Required]
        public DataFormat Format { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
