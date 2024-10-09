using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Crud.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        public int Money { get; set; }
        [JsonIgnore]
        public List<ProductPurchase> Purchase { get; set; }
    }
}
