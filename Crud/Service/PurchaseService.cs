using AutoMapper;
using Crud.DTO;
using Crud.Models;
using Crud.Repository.IRepository;
using Crud.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Service
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IProductPurchaseRepository _productPurchaseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public PurchaseService(
            IProductPurchaseRepository productPurchaseRepository,
            IUserRepository userRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _productPurchaseRepository = productPurchaseRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public IActionResult BuyProduct(ProductPurchaseDTO productPurchaseDto)
        {
            try
            {
                // Validaciones de datos del DTO
                if (productPurchaseDto == null || !productPurchaseDto.Products.Any())
                {
                    return new BadRequestObjectResult("Product purchase cannot be null or empty.");
                }

                // Validación existencia usuario
                var user = _userRepository.GetById(productPurchaseDto.UserID);
                if (user == null)
                {
                    return new UnauthorizedObjectResult(new { success = false, message = "User credentials error." });
                }

                // Calcular el total de la compra
                int totalCost = productPurchaseDto.Products.Sum(p => p.Price * p.Quantity);

                // Validación de fondos disponibles
                if (totalCost > user.Money)
                {
                    return new BadRequestObjectResult("Insufficient funds for this purchase.");
                }

                // Actualizar el dinero del usuario
                user.Money -= totalCost;
                _userRepository.update(user);
                _userRepository.Save(); // Guardamos los cambios en el usuario

                // Crear la compra
                var purchase = _mapper.Map<ProductPurchase>(productPurchaseDto);
                purchase.Products = new List<Product>();

                // Actualizar stock de productos y añadir a la compra
                foreach (var products in productPurchaseDto.Products)
                {
                    var productFromDb = _productRepository.GetProductsByNames(products.Name);
                    if (productFromDb == null || productFromDb.Price != products.Price )
                    {
                        return new NotFoundObjectResult($"Product not found.");
                    }

                    if (productFromDb.Quantity < products.Quantity)
                    {
                        return new BadRequestObjectResult($"Not enough stock for product: {productFromDb.Name}");
                    }

                    // Actualizar cantidad y añadir producto a la compra
                    productFromDb.Quantity -= products.Quantity;
                    _productRepository.update(productFromDb);


                    // Añadir el nuevo producto a la compra
                    var productUser = productFromDb;
                    productUser.Quantity = products.Quantity;
                    purchase.Products.Add(productUser);
                }

                // Guardar la compra

                _productPurchaseRepository.Add(purchase);
                _productPurchaseRepository.Save();

                return new CreatedResult($"/purchase/{purchase.Id}", new { purchase });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return new StatusCodeResult(500);
            }
        }

        public IActionResult ListPurchase()
        {
            try
            {
                var purchases = _productPurchaseRepository.GetAll(includeProperties: "Products");
                if (!purchases.Any())
                {
                    return new BadRequestObjectResult("There are no registered purchases.");
                }
                return new OkObjectResult(purchases);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }

        public IActionResult ListPruchasesById(int userId)
        {
            try
            {
                var purchases = _productPurchaseRepository.GetAll(includeProperties:"Products",filter: p => p.UserID == userId);
                if (!purchases.Any())
                {
                    return new BadRequestObjectResult("There are no registered purchases.");
                }
                return new OkObjectResult(purchases);
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
