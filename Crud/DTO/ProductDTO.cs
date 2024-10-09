using System.ComponentModel.DataAnnotations;

namespace Crud.DTO
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Total must be a positive number.")]
        [Required(ErrorMessage = "Price is required")]
        public int Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Total must be a positive number.")]
        [Required(ErrorMessage = "quantity is required")]
        public int Quantity { get; set; }
    }
}
