using CatalogService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMyCatalogService _myCatalogService;

        public CategoriesController(ILogger<CategoriesController> logger, IMyCatalogService myCatalogService)
        {
            _logger = logger;
            _myCatalogService = myCatalogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _myCatalogService.GetCategories().ConfigureAwait(true);

            if (categories == null || categories.Count == 0)
                return NotFound();

            return Ok(categories);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] Category category)
        {
            Category createdCategory = await _myCatalogService.AddCategory(category).ConfigureAwait(true);

            return Ok(createdCategory);
        }

        [HttpPut("{categoryId:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromBody] Category category)
        {
            Category updatedCategory = await _myCatalogService.UpdateCategory(categoryId, category).ConfigureAwait(true);

            return Ok(updatedCategory);
        }

        [HttpDelete("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            Category category = await _myCatalogService.GetCategoryById(categoryId).ConfigureAwait(true);

            if (category == null)
                return NotFound();

            await _myCatalogService.DeleteCategory(category).ConfigureAwait(true);

            return NoContent();
        }
    }
}