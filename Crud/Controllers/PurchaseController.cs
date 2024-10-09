using Crud.DTO;
using Crud.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    //[Authorize]
    [Route("api/purchases")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _productPurchaseService;
        public PurchaseController(IPurchaseService productPurchaseService)
        {
            _productPurchaseService = productPurchaseService;
        }
        [HttpGet]
        public IActionResult GetAll() 
        { 
            var result = _productPurchaseService.ListPurchase();
            return result;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductPurchaseDTO product)
        {
            var result =  _productPurchaseService.BuyProduct(product);
            return result;
        }
        [HttpGet("{userId:int}")]
        public IActionResult GetPurchasesByUserId(int userId) 
        { 
            var result = _productPurchaseService.ListPruchasesById(userId);
            return result;
        }
    }
}
