using Domain.Interfaces;

namespace Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        ICustomerSaleRepository CustomerSaleRepository { get; }
        Task<int> SaveAsync();
    }
}
