using CatalogService.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatalogService.Controllers
{
    [Route("Categories")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IMyCatalogService _myCatalogService;

        public ItemsController(IMyCatalogService myCatalogService)
        {
            _myCatalogService = myCatalogService;
        }

        [HttpGet("{categoryId:int}/items")]
        public async Task<IActionResult> GetItems([FromRoute] int categoryId, [FromQuery] ItemParameters itemParameters)
        {
            var items = await _myCatalogService.GetItems(categoryId, itemParameters).ConfigureAwait(true);

            if (items == null || items.Count == 0)
                return NotFound();
            return Ok(items);
        }

        
        [HttpPost("{categoryId:int}/items")]
        public async Task<IActionResult> Post([FromRoute] int categoryId, [FromBody] Item item)
        {
            item.CategoryId = categoryId;
            Item createdItem = await _myCatalogService.AddItem(item).ConfigureAwait(true);

            return Ok(createdItem);
        }

        [HttpPut("{categoryId:int}/items/{itemId:int}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int categoryId, [FromRoute] int itemId, [FromBody] Item item)
        {
            Item updatedItem = await _myCatalogService.UpdateItem(itemId, item).ConfigureAwait(true);

            return Ok(updatedItem);
        }

        [HttpDelete("{categoryId:int}/items/{itemId:int}")]
        public async Task<IActionResult> DeleteItem([FromRoute] int itemId)
        {
            Item item = await _myCatalogService.GetItemById(itemId).ConfigureAwait(true);

            if (item == null)
                return NotFound();

            await _myCatalogService.DeleteItem(item).ConfigureAwait(true);

            return NoContent();
        }
    }
}
