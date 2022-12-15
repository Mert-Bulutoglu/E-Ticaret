using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly StoreContext _context;

        public CategoryRepository(StoreContext context)
        {
            _context = context;
        }

        public void Add(Category category)
        {
             _context.Add(category);
        }

        public void Delete(Category category)
        {
            _context.Remove(category);
        }

        public async Task<IReadOnlyList<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<bool> SaveChangesAsync()
        {
             return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public void Update(Category category)
        {
           _context.Attach(category);
           _context.Entry(category).State = EntityState.Modified;
        }
    }
}