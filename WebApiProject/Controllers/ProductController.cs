using Application.Interfaces;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly IProductService _productService;
        public ProductController(IUnitOfWork unitOfWork, IProductService productService) : base(unitOfWork)
        { 
            _productService = productService; 
        }

        [Route("GetProducts")]
        [HttpGet]
        public async Task<IActionResult> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [Route("AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProductAsync(Product product)
        {
            await _productService.AddProductAsync(product);
            return Created("api/product/{product.Id}",new { message = "Created successfully" });
        }
    }
}
