using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Utils;
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
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var orderDetails = await _repository.GetOrderDetailsAsync();
                var mappedOrderDetails = _mapper.Map<IReadOnlyList<OrderDetail>, IReadOnlyList<OrderDetailToReturnDto>>(orderDetails);
                var response = new Response(true, mappedOrderDetails, null);
                return Ok(response);
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailToReturnDto>> GetOrderDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var orderDetail = await _repository.GetOrderDetailByIdAsync(id);
                var mappedOrderDetail = _mapper.Map<OrderDetail, OrderDetailToReturnDto>(orderDetail);
                if (mappedOrderDetail == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Order Detail not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    var response = new Response(true, mappedOrderDetail, null);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);

            }
        }

        [HttpPost]
        public async Task<ActionResult> AddOrderDetail(OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (orderDetail.OrderId == 0 )
                {
                    var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Fields cannot be empty.");
                    var response = new Response(false, null, responseError);
                    return BadRequest(response);
                }
                else
                {
                    _repository.Add(orderDetail);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, orderDetail, null);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrderDetail(int id, OrderDetail orderDetail)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (id != orderDetail.Id)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Order Detail not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    _repository.Update(orderDetail);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, orderDetail, null);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrderDetail(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var orderDetail = await _repository.GetOrderDetailByIdAsync(id);
                if (orderDetail == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Order Detail not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    _repository.Delete(orderDetail);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, orderDetail, null);
                    return Ok(response);
                }
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}