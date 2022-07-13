using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyFirstWebApp.Database.Entities
{
    [Table("APISeller")]
    public class ApiSeller
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname  { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        [ForeignKey("SellerId")]
        public ICollection<ApiProcessor> Processors { get; set; }
    }
}
