using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<bool> SaveChangesAsync();
        void Add(Product product);
        void Update(Product product);
        void Delete(Product product);

    }
}