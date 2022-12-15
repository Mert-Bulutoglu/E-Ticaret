using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderRepository : IOrderRepository
    {
        private readonly StoreContext _context;

        public OrderRepository(StoreContext context)
        {
            _context = context;
        }


        public void Add(Order order)
        {
            _context.Add(order);
        }

        public void Delete(Order order)
        {
            _context.Remove(order);
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(p => p.Customer)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(p => p.Customer).
            ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public void Update(Order order)
        {
            _context.Attach(order);
            _context.Entry(order).State = EntityState.Modified;
        }
    }
}