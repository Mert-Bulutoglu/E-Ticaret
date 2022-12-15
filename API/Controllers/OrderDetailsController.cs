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
    public class OrderDetailsController : BaseApiController
    {
        public IOrderDetailRepository _repository;
        private readonly IMapper _mapper;

        public OrderDetailsController(IOrderDetailRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderDetailToReturnDto>>> GetOrderDetails()
        {

            var orderDetails = await _repository.GetOrderDetailsAsync();
            var mappedOrderDetails = _mapper.Map<IReadOnlyList<OrderDetail>, IReadOnlyList<OrderDetailToReturnDto>>(orderDetails);
            return Ok(mappedOrderDetails);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailToReturnDto>> GetOrderDetail(int id)
        {
            var orderDetail = await _repository.GetOrderDetailByIdAsync(id);
            return _mapper.Map<OrderDetail, OrderDetailToReturnDto>(orderDetail);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderDetail(OrderDetail orderDetail)
        {
            _repository.Add(orderDetail);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderDetail(string id, OrderDetail orderDetail)
        {
            _repository.Update(orderDetail);
            return Ok(await _repository.SaveChangesAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderDetail(int id)
        {
            var orderDetail = await _repository.GetOrderDetailByIdAsync(id);
            _repository.Delete(orderDetail);
            return Ok(await _repository.SaveChangesAsync());
        }
    }
}