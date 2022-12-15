using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly StoreContext _context;

        public OrderDetailRepository(StoreContext context)
        {
            _context = context;
        }
 

        public void Add(OrderDetail orderDetail)
        {
             _context.Add(orderDetail);
        }

        public void Delete(OrderDetail orderDetail)
        {
            _context.Remove(orderDetail);
        }

        public async Task<OrderDetail> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails
            .Include(p => p.Product)
            .Include(p => p.Order)
            .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IReadOnlyList<OrderDetail>> GetOrderDetailsAsync()
        {
             return await _context.OrderDetails
             .Include(p => p.Product)
             .Include(p => p.Order)
             .ToListAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync().ConfigureAwait(false) > 0;
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.Attach(orderDetail);
            _context.Entry(orderDetail).State = EntityState.Modified;
        }
    }
}