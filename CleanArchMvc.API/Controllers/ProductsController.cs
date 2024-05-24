using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            var products = await _productService.GetAllAsync();
            if (products == null)
            {
                return NotFound("Products not found!");
            }
            return Ok(products);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound("Product not found!");
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDTO)
        {
            if (productDTO == null)
            {
                return BadRequest("Invalid Data");
            }

            productDTO = await _productService.CreateAsync(productDTO);

            return CreatedAtAction(nameof(GetById), new { id = productDTO.Id }, productDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO productDTO)
        {
            if (productDTO == null || productDTO.Id <= 0)
            {
                return BadRequest("Invalid Data");
            }

            await _productService.UpdateAsync(productDTO);
            return Ok(productDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound("Product not found!");
            }
            await _productService.RemoveAsync(id);
            return Ok(product);
        }
    }
}
