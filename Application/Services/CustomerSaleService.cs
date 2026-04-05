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
    public class CustomerSaleService : ICustomerSaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CustomerSaleService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CustomerSale>> GetAllCustomerSaleAsync()
        {
            var result = await _unitOfWork.CustomerSaleRepository.GetAllCustomerSaleAsync();
            
            //Get sales greater than 6000
            var mResult1 = result.Where(s => s.Amount > 6000).ToList();
            var qResult1 = (from s in result
                          where s.Amount > 6000
                          select s).ToList();

            //Get sales done in 2024
            var mResult2 = result.Where(s => s.SaleDate.Year == 2024).ToList();
            var qResult2 = (from s in result
                            where s.SaleDate.Year == 2024
                            select s).ToList();

            //Get sales done in January
            var mResult3 = result.Where(s => s.SaleDate.Month == 1).ToList();
            var qResult = (from s in result
                           where s.SaleDate.Month == 1
                           select s).ToList();

            //Get sales on specific date
            var mResult4 = result.Where(s => s.SaleDate.Date == new DateTime(2026, 03, 11)).ToList();
            var qResult4 = (from s in result
                            where s.SaleDate.Date == new DateTime(2026, 03, 11)
                            select s).ToList(); 

            //Get sales amount between 5000 and 9000
            var mResult5 = result.Where(s => s.Amount >= 5000 && s.Amount <= 9000).ToList();
            var qResult5 = (from s in result
                            where s.Amount>=5000 && s.Amount<=9000
                            select s).ToList();

            //Get highest sales amount
            var mResult6 = result.Max(s => s.Amount);
            var qResult6 = (from s in result
                           select s).Max();

            //Get sale having highest amount
            var mResult7 = result.OrderByDescending(s => s.Amount).FirstOrDefault();
            var qResult7 = (from s in result
                            orderby s.Amount descending
                            select s).FirstOrDefault();

            //Get sales in current year
            var mResult8 = result.Where(s => s.SaleDate.Year == DateTime.Now.Year).ToList();
            var qResult8 = (from s in result
                            where s.SaleDate.Year == DateTime.Now.Year
                            select s).ToList();

            //Count sales in 2024
            var mResult9 = result.Count(s => s.SaleDate.Year == 2026);
            var qResult9 = (from s in result
                            select s).Count();

            //Get average sales amount
            var mResult10 = result.Average(s => s.Amount);
            var qResult10 = (from s in result
                             select s.Amount).Average();

            //Group sales by year
            var mResult11 = result.GroupBy(s => s.SaleDate.Year)
                                 .Select(g => new
                                 {
                                     Year = g.Key,
                                     Sales = g.Count()
                                 });
            var qResult11 = (from s in result
                             group s by s.SaleDate.Year into g
                             select new
                             {
                                 Year = g.Key,
                                 Sales = g.Count()
                             }).ToList();

            //Get second highest sale amount
            var mResult12 = result.OrderByDescending(s => s.Amount)
                                 .Select(s => s.Amount)
                                 .Distinct()
                                 .Skip(1)
                                 .FirstOrDefault();
            var qResult12 = (from s in result
                             orderby s.Amount descending
                             select s.Amount)
                             .Distinct()
                             .Skip(1)
                             .FirstOrDefault();

            //Get top 3 sales
            var mResult13 = result.OrderByDescending(s => s.Amount)
                                 .Select(s => s.Amount)
                                 .Distinct()
                                 .Take(3).ToList();
            var qResult13 = (from s in result
                             orderby s.Amount descending
                             select s.Amount)
                             .Distinct()
                             .Take(3).ToList();

            //Get sales in last 30 days
            var mResult14 = result.Where(x => x.SaleDate >= DateTime.Now.AddDays(-30)).ToList();
            var qResult14 = (from s in result
                             where s.SaleDate >= DateTime.Now.AddDays(-30)
                             select s).ToList();

            //Get total sale amount by city
            var mResult15 = result.GroupBy(s => s.City)
                                 .Select(g => new
                                 {
                                     City = g.Key,
                                     TotalSales = g.Sum(s => s.Amount)
                                 });
            var qResult15 =  from s in result
                             group s by s.City into g
                             select new
                             {
                                 City = g.Key,
                                 TotalSales = g.Sum(x => x.Amount)
                             };

            //Sales done in current month
            var mResult16 = result.Where(s=>s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year).ToList();
            var qResult16 = (from s in result
                             where s.SaleDate.Month == DateTime.Now.Month && s.SaleDate.Year == DateTime.Now.Year
                             select s).ToList();

            //Sales per customer
            var mResult17 = result.GroupBy(s => s.CustomerName)
                                 .Select(g => new
                                 {
                                     customer = g.Key,
                                     sales = g.Sum(s => s.Amount)
                                 });
            var qResult17 = from s in result
                            group s by s.CustomerName into g
                            select new
                            {
                                Customer = g.Key,
                                Sales = g.Sum(s => s.Amount)
                            };

            //Highest purchase per customer
            var mResult18 = result.GroupBy(s => s.CustomerName)
                                 .Select(g => new
                                 {
                                     customer = g.Key,
                                     highestPurchase = g.OrderByDescending(s => s.Amount).FirstOrDefault()
                                 });
            var qResult18 = from s in result
                            group s by s.CustomerName into g
                            select new
                            {
                                Customer = g.Key,
                                HighestPurchase = g.Max(s => s.Amount)
                            };

            //Third highest salary
            var mResult19 = result.OrderByDescending(s => s.Amount)
                                 .Select(x => x.Amount)
                                 .Distinct()
                                 .Skip(2)
                                 .FirstOrDefault();
            var qResult19 = (from s in result
                            orderby s.Amount descending
                            select s.Amount)
                            .Distinct()
                            .Skip(2)
                            .FirstOrDefault();

            //Group sales by month
            var mResult20 = result.GroupBy(s => new { s.SaleDate.Year, s.SaleDate.Month })
                                  .Select(g => new
                                  {
                                      g.Key.Year,
                                      g.Key.Month,
                                      Sales = g.Sum(s => s.Amount)
                                  });
            var qResults20 = from s in result
                             group s by new { s.SaleDate.Year, s.SaleDate.Month } into g
                             select new
                             {
                                 g.Key.Year,
                                 g.Key.Month,
                                 Sales = g.Sum(x => x.Amount)
                             };

            //Customer Who Spent the Most in Last Year
            var mResults20 = result.Where(s => s.SaleDate.Year == DateTime.Now.Year - 1)
                                   .GroupBy(g => g.CustomerName)
                                   .Select(x => new
                                   {
                                       Customer = x.Key,
                                       TotalSpent = x.Sum(s => s.Amount)
                                   })
                                   .OrderByDescending(s=>s.TotalSpent)
                                   .FirstOrDefault();
            var qResult20 = (from s in result
                           where s.SaleDate.Year == DateTime.Now.Year - 1
                           group s by s.CustomerName into g
                           select new
                           {
                               Customer = g.Key,
                               TotalSpent = g.Sum(x => x.Amount)
                           })
                          .OrderByDescending(s => s.TotalSpent).FirstOrDefault();

            //Inner join
            var presult = await _unitOfWork.ProductRepository.GetAllProductsAsync();
            var qResult21 = from s in result
                            join p in presult
                            on s.SaleId equals p.Id
                            select new
                            {
                                s.CustomerName,
                                s.Amount,
                                p.Name
                            };

            //Left join
            var qResult22 = from s in result
                            join p in presult
                            on s.SaleId equals p.Id into res
                            from finalres in res.DefaultIfEmpty()
                            select new
                            {
                                s.CustomerName,
                                s.Amount,
                                finalres.Name
                            };

            //Right join: no direct right join - done via reverse of left join
            var presult23 = from p in presult
                            join s in result
                            on p.Id equals s.SaleId into res
                            from finalres in res.DefaultIfEmpty()
                            select new
                            {
                                p.Name,
                                finalres.CustomerName,
                                finalres.Amount
                            };

            //Full outer join
            var leftJoin = from s in result
                           join p in presult
                           on s.SaleId equals p.Id into res
                           from finalres in res.DefaultIfEmpty()
                           select new
                           {
                               s.CustomerName,
                               s.Amount,
                               finalres.Name
                           };
            var rightJoin = from p in presult
                            join s in result
                            on p.Id equals s.SaleId into res
                            from finalres in res.DefaultIfEmpty()
                            select new
                            {
                                finalres.CustomerName,
                                finalres.Amount,
                                p.Name
                            };
            var finalresult = leftJoin.Union(rightJoin);

            return result;
        }

        public async Task AddCustomerSaleAsync(CustomerSale customerSale)
        {
            await _unitOfWork.CustomerSaleRepository.AddCustomerSaleAsync(customerSale);
            await _unitOfWork.SaveAsync();
        }
    }
}
