using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DaHo.M151.DataFormatValidator.Models.StorageModels
{
    public class DataSchema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("format")]
        public DataFormat ForFormat { get; set; }

        [Column("name")]
        public string SchemaName { get; set; }

        [Column("content")]
        public string Schema { get; set; }
    }
}
