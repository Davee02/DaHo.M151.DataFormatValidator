namespace DaHo.M151.DataFormatValidator.Models.ApiModels
{
    public class ConvertFormatRequest
    {
        public DataFormat From { get; set; }

        public DataFormat To { get; set; }

        public string Content { get; set; }
    }
}
