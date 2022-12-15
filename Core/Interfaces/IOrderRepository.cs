

using Core.Entities;

namespace Core.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IReadOnlyList<Order>> GetOrdersAsync();
        Task<bool> SaveChangesAsync();
        void Add(Order order);
        void Update(Order order);
        void Delete(Order order);

    }
}