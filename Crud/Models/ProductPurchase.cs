using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Crud.Models
{
    public class ProductPurchase
    {

        [Key]
        public int Id { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Total {  get; set; }
        public List<Product> Products { get; set; }
        public int UserID { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public ProductPurchase()
        {
            PurchaseDate = DateTime.UtcNow;
        }
    }
}
