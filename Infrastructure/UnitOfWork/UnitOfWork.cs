using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;
        public IProductRepository ProductRepository { get; }
        public ICustomerSaleRepository CustomerSaleRepository { get; }

        public UnitOfWork(AppDbContext appDbContext, IProductRepository productRepository, ICustomerSaleRepository customerSaleRepository) 
        {
            _appDbContext = appDbContext;
            ProductRepository = productRepository;
            CustomerSaleRepository = customerSaleRepository;
        }

        public async Task<int> SaveAsync()
        {
            return await _appDbContext.SaveChangesAsync();
        }
    }
}
