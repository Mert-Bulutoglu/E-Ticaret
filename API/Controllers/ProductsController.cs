using API.Dtos;
using API.Utils;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        public IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var products = await _repository.GetProductsAsync();
                var mappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);
                return Ok(mappedProducts);
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                var mappedProduct = _mapper.Map<Product, ProductToReturnDto>(product);
                if (mappedProduct == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    var response = new Response(true, mappedProduct, null);
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
        public async Task<ActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                _repository.Add(product);
                await _repository.SaveChangesAsync();
                var response = new Response(true, product, null);
                return Ok(response);
            }
            catch (Exception e)
            {
                var responseError = new ResponseError(StatusCodes.Status500InternalServerError, e.Message);
                var response = new Response(false, null, responseError);
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                if (id != product.Id)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    _repository.Update(product);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, product, null);
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
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                var responseError = new ResponseError(StatusCodes.Status400BadRequest, "Invalid model");
                var response = new Response(false, ModelState, responseError);
                return BadRequest(response);
            }
            try
            {
                var product = await _repository.GetProductByIdAsync(id);
                if (product == null)
                {
                    var responseError = new ResponseError(StatusCodes.Status404NotFound, "Product not found.");
                    var response = new Response(false, null, responseError);
                    return NotFound(response);
                }
                else
                {
                    _repository.Delete(product);
                    await _repository.SaveChangesAsync();
                    var response = new Response(true, product, null);
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