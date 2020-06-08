using System.ComponentModel.DataAnnotations;

namespace DaHo.M151.DataFormatValidator.Models.ApiModels
{
    public class DataSchema
    {
        [Required]
        public DataFormat ForFormat { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string Schema { get; set; }
    }
}
