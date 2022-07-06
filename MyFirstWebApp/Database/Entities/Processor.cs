using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebApp.Database.Entities
{
    [Table("Processors")]
    public class Processor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string PictureUrl { get; set; }
        public decimal Price { get; set; }
        public string ProductLink { get; set; }
    }
}
