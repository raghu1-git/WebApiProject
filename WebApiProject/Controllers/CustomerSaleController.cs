using Application.Interfaces;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    public class CustomerSaleController: BaseController
    {
        private readonly ICustomerSaleService _customerSaleService;
        public CustomerSaleController(IUnitOfWork unitOfWork, ICustomerSaleService customerSaleService) : base(unitOfWork)
        {
            _customerSaleService = customerSaleService;
        }

        [Route("GetCustomerSale")]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomerSaleAsync()
        {
            var products = await _customerSaleService.GetAllCustomerSaleAsync();
            return Ok(products);
        }

        [Route("AddCustomerSale")]
        [HttpPost]
        public async Task<IActionResult> AddCustomerSaleAsync(CustomerSale customerSale)
        {
            await _customerSaleService.AddCustomerSaleAsync(customerSale);
            return Created("api/customeSale/{CustomerSale.saleId}", new { message = "Created successfully." });
        }
    }
}
