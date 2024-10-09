using Crud.DTO;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Service.IService
{
    public interface IProductService
    {
        public IActionResult Create(ProductDTO product);
        public IActionResult ListProduct();
        public IActionResult Edit(ProductDTO product, int id);
        public IActionResult GetProduct(int id);
        public Task<IActionResult> Delete(int id);
    }
}
