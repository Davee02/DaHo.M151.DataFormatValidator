using System.ComponentModel.DataAnnotations;

namespace DaHo.M151.DataFormatValidator.Models.ApiModels
{
    public class ValidateFormatRequest
    {
        [Required]
        public DataFormat Format { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Content { get; set; }
    }
}
