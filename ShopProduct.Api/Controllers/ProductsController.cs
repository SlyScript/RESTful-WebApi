using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ShopProduct.Api.Dtos;
using ShopProduct.Api.Entities;
using ShopProduct.Api.Repositories;
using ShopProduct.Api.Repositories.Contracts;

namespace ShopProduct.Api.Controllers
{
    [EnableCors]
    [Route("api/shop/")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<ProductReadDto>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();

                if(!products.Any())
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Products Fetch failed: {ex.Message}");
            }
            
        }

        [HttpGet]
        [Route("product/{id}")]
        public async Task<ActionResult<ProductReadDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductAsync(id);

                if (product == null)
                {
                    return NotFound();
                }
                return Ok(_mapper.Map<ProductReadDto>(product));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Product Fetch failed: {ex.Message}"); 
            }
        }

        [HttpPost]
        [Route("product")]
        public async Task<ActionResult> AddProduct(ProductAddDto productAddDto)
        {
            try
            {
               var product = _mapper.Map<Product>(productAddDto);

               await _productRepository.AddProductAsync(product);

               return Ok(); 
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Product Addition failed: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("product/{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductUpdateDto productUpdateDto)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductAsync(id);

                if (existingProduct == null)
                {
                    return NotFound();
                }

                existingProduct.Name = productUpdateDto.Name;
                existingProduct.Description = productUpdateDto.Description;
                existingProduct.Price = productUpdateDto.Price;
                existingProduct.Quantity = productUpdateDto.Quantity;
                           
                var updatedProduct = _mapper.Map<Product>(existingProduct);
                await _productRepository.UpdateProductAsync(updatedProduct);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Product Update failed: {ex.Message}");
            }
        }

        [HttpDelete]
        [Route("product/{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id >= 1)
                {
                    await _productRepository.DeleteProductAsync(id);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Product Delete failed: {ex.Message}");
            }
        }
    }
}
