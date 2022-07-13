using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebApp.Database.Entities
{
    [Table("APIProcessors")]
    public class ApiProcessor
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PictureUrl { get; set; }

        public decimal Price { get; set; }

        public string ProductLink { get; set; }


        public int SellerId { get; set; }

        public ApiSeller ApiSeller { get; set; }
    }
}
