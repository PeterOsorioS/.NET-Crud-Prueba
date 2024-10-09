using Crud.Models;
using System.ComponentModel.DataAnnotations;

namespace Crud.DTO
{
    public class ProductPurchaseDTO
    {
        public List<ProductDTO> Products { get; set; }
        public int UserID { get; set; }
    }
}
