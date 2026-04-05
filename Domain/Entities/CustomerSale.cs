using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CustomerSale
    {
        [Key]
        public int SaleId { get; set; }
        public string? CustomerName { get; set; }
        public string? City { get; set; }
        public decimal Amount { get; set; }
        public DateTime SaleDate { get; set; }
    }
}
