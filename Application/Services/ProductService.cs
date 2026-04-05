using Application.Interfaces;
using Domain.Entities;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.ProductRepository.GetAllProductsAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.AddProductAsync(product);
            await _unitOfWork.SaveAsync();
        }
    }
}
