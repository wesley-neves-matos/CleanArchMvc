using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchMvc.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            if (categories == null)
            {
                return NotFound("Categories not found!");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound("Category not found!");
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid Data");
            }

            categoryDTO = await _categoryService.CreateAsync(categoryDTO);

            return CreatedAtAction(nameof(GetById), new { id = categoryDTO.Id }, categoryDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDTO categoryDTO)
        {
            if (categoryDTO == null || categoryDTO.Id <= 0)
            {
                return BadRequest("Invalid Data");
            }

            await _categoryService.UpdateAsync(categoryDTO);
            return Ok(categoryDTO);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound("Category not found!");
            }
            await _categoryService.RemoveAsync(id);
            return Ok(category);
        }
    }
}
