using Crud.DTO;
using Crud.Models;
using Crud.Repository.IRepository;
using Crud.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetAll() 
        { 
            var result = _productService.ListProduct();
            return result;
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _productService.GetProduct(id);
            return result;
        }

        [HttpPost]
        public IActionResult Create([FromBody]ProductDTO product) 
        {
            var result = _productService.Create(product);
            return result;
        }

        [HttpPut("{id:int}")]
        public IActionResult Update(int id,[FromBody]ProductDTO product)
        {
            var result = _productService.Edit(product, id);
            return result;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) 
        { 
            var result = await _productService.Delete(id);
            return result;
        }
    }
}
