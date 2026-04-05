using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly AppDbContext _appDbContext;
        public ProductRepository(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
        }

        
    }
}
