using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerSaleRepository
    {
        Task<IEnumerable<CustomerSale>> GetAllCustomerSaleAsync();
        Task AddCustomerSaleAsync(CustomerSale customerSale);
    }
}
