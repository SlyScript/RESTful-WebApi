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
    [Route("api/products")]
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
        public async Task<ActionResult<ProductReadDto>> GetAllProducts()
        {
            try
            {
                var products = await _productRepository.GetAllProductsAsync();

                if(products == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_mapper.Map<IEnumerable<ProductReadDto>>(products));
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data from the database");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductReadDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetProductAsync(id);

                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(_mapper.Map<ProductReadDto>(product));
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retreiving data from the database");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostProduct(ProductAddDto productAddDto)
        {
            /*if(this.HttpContext.Request.Method == "OPTIONS")
            {
                Response.Headers.Add("Access-Control-Allow-Origin", "*");
                return Ok();
            }*/
            try
            {
               var product = _mapper.Map<Product>(productAddDto);

               await _productRepository.AddProductAsync(product);

                return Ok(); 
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding data to the database");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(int id, ProductUpdateDto productUpdateDto)
        {
            try
            {
                var existingProduct = await _productRepository.GetProductAsync(id);

                existingProduct.Name = productUpdateDto.Name;
                existingProduct.Description = productUpdateDto.Description;
                existingProduct.Price = productUpdateDto.Price;
                existingProduct.Quantity = productUpdateDto.Quantity;
                           
                var updatedProduct = _mapper.Map<Product>(existingProduct);
                await _productRepository.UpdateProductAsync(updatedProduct);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data to the database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id >= 0)
                {
                    await _productRepository.DeleteProductAsync(id);
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data from the database");
            }
        }
    }
}
