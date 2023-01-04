using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class ProductRepository : IProductRepository
    {
       private readonly StoreContext _context;
        
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(Product product)
        {
            _context.Add(product);

        }
        
        public void Delete(Product product)
        {
            _context.Remove(product);
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(c => c.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).
            ToArrayAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public void Update(Product product)
        {
            _context.Attach(product);
            _context.Entry(product).State = EntityState.Modified;
        }
    }
}