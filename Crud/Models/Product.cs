using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Crud.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
