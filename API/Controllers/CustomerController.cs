using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class CustomerController : BaseApiController
    {
         public ICustomerRepository _repository;
         private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<CustomerDto>>> GetCustomers()
        {

            var customers = await _repository.GetCustomersAsync();
            var mappedCustomers = _mapper.Map<IReadOnlyList<Customer>, IReadOnlyList<CustomerDto>>(customers);
            return Ok(mappedCustomers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetCustomer(int id)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);
            return _mapper.Map<Customer, CustomerDto>(customer);
        }

        [HttpPost]
        public async Task<ActionResult> AddCustomer(Customer customer)
        {
            _repository.Add(customer);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(string id, Customer customer)
        {
            _repository.Update(customer);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            var customer = await _repository.GetCustomerByIdAsync(id);
            _repository.Delete(customer);
            return Ok(await _repository.SaveChangesAsync());
        }
        
    }
}