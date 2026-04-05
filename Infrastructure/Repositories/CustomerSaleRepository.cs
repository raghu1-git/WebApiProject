using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Caching;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class CustomerSaleRepository : ICustomerSaleRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly ICachingService _cachingService;
        public CustomerSaleRepository(AppDbContext appDbContext, ICachingService cachingService) 
        {
            _appDbContext = appDbContext;
            _cachingService = cachingService;
        }

        public async Task<IEnumerable<CustomerSale>> GetAllCustomerSaleAsync()
        {
            IEnumerable<CustomerSale>? custData = null;
            if (_cachingService != null)
            {
                custData = await _cachingService.GetCacheAsync<List<CustomerSale>>("custdata1");
                if(custData != null && custData.Count() > 0)
                    return custData;

                custData = await _appDbContext.CustomerSales.ToListAsync();
                await _cachingService.SetCacheAsync("custdata1", custData, 5);
            }
            return custData!;
        }

        public async Task AddCustomerSaleAsync(CustomerSale customerSale)
        {
            await _appDbContext.CustomerSales.AddAsync(customerSale);
        }

        
    }
}
