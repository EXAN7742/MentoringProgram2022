using CatalogService.Models;

namespace CatalogService
{
    public interface IMyCatalogService
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int categoryId);
        Task<Category> AddCategory(Category category);
        Task<Category> UpdateCategory(int categoryId, Category category);
        Task DeleteCategory(Category category);

        Task<List<Item>> GetItems(int categoryId, ItemParameters itemParameters);
        Task<Item> GetItemById(int itemId);
        Task<Item> AddItem(Item item);
        Task<Item> UpdateItem(int itemId, Item item);
        Task DeleteItem(Item item);
    }
}
