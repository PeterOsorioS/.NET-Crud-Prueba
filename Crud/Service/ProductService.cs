using AutoMapper;
using Crud.DTO;
using Crud.Helpers;
using Crud.Models;
using Crud.Repository.IRepository;
using Crud.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IActionResult ListProduct()
        {
            try
            {
                var ListProduct = _productRepository.GetAll();
                if (!ListProduct.Any())
                {
                    return new BadRequestObjectResult("There are no registered products.");
                }
                return new OkObjectResult(ListProduct);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        public IActionResult Create(ProductDTO product)
        {
            try
            {
                if (product == null)
                {
                    return new BadRequestObjectResult("Product cannot be null.");
                }
                // Mappeo dto a modelo
                var newProduct = _mapper.Map<Product>(product);

                _productRepository.Add(newProduct);
                _productRepository.Save();
                return new CreatedResult($"/products/{newProduct.Id}", new { newProduct });
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        public IActionResult Edit(ProductDTO product, int id)
        {
            try
            {
                if (product == null)
                {
                    return new BadRequestObjectResult("Product data is invalid.");
                }

                var productFromDb = _productRepository.GetById(id);
                if (productFromDb == null)
                {
                    return new BadRequestObjectResult("Product doesn't exist.");
                }

                // Mappeo producto
                _mapper.Map(product,productFromDb);


                _productRepository.update(productFromDb);
                _productRepository.Save();


                return new OkObjectResult("Updated product.");
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _productRepository.GetById(id);
                if(product == null)
                {
                    return new BadRequestObjectResult("The product doesn't exist.");
                }
                return new OkObjectResult(product);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var product = _productRepository.GetById(id);
                if (product == null)
                {
                    return new BadRequestObjectResult("Product doesn't exist");
                }
                await _productRepository.Remove(product);
                await _productRepository.SaveAsync();

                return new OkObjectResult("deleted successfully");
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
