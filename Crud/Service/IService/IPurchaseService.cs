using Crud.DTO;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Service.IService
{
    public interface IPurchaseService
    {
        public IActionResult BuyProduct(ProductPurchaseDTO productPurchase);
        public IActionResult ListPurchase();
        public IActionResult ListPruchasesById(int userId);
    }
}
