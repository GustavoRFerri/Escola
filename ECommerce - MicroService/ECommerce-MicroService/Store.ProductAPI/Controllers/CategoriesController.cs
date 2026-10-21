using Microsoft.AspNetCore.Mvc;
using Store.ProductAPI.DTOs;
using Store.ProductAPI.Services;

namespace Store.ProductAPI.Controllers
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
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();

            if (categories == null)            
                return NotFound(" Categorias not found ");            

            return Ok(categories);
        }


        [HttpGet(" Categories ")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoriesProducts()
        {
            var categories = await _categoryService.GetCategoriesProducts();
            if (categories == null)
                return NotFound(" Categories not found ");

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = " GetCategoryById ")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound(" Categories not found ");
            }

            return Ok(category);

        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CategoryDTO categoryDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _categoryService.AddCategory(categoryDTO);
            return new CreatedAtRouteResult(" GetCategoryById ", new { id = categoryDTO.CategoryId }, categoryDTO);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCategory(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest("Category ID mismatch");
            }
            if (categoryDTO is null)
            {
                return BadRequest();
            }
            await _categoryService.UpdateCategory(categoryDTO);
            return Ok(categoryDTO);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            await _categoryService.RemoveCategory(id);
            return Ok(category);
        }


    }
}
