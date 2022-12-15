using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<IReadOnlyList<Customer>> GetCustomersAsync();
        Task<bool> SaveChangesAsync();
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
    }
}