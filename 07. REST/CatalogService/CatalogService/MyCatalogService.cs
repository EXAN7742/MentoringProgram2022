using CatalogService.Models;

namespace CatalogService
{
    public class MyCatalogService: IMyCatalogService
    {
        public List<Category> Categories { get; set; }
        public List<Item> Items { get; set; }

        public MyCatalogService()
        {
            Categories = new List<Category>();
            Categories.Add(new Category() { Id = 1, Name = "First category" });

            Items = new List<Item>();
            Items.Add(new Item() { Id = 1, Name = "First item", CategoryId = 1 });
            Items.Add(new Item() { Id = 2, Name = "Second item", CategoryId = 1 });
            Items.Add(new Item() { Id = 3, Name = "Third item", CategoryId = 1 });
            Items.Add(new Item() { Id = 4, Name = "Fourth item", CategoryId = 1 });
        }

        public async Task<List<Category>> GetCategories()
        {
            return await Task.Run(() => { return Categories; });
        }
        public async Task<Category> GetCategoryById(int categoryId)
        {
            return await Task.Run(() => { return Categories.Where(c => c.Id == categoryId).FirstOrDefault(); });
        }
        public async Task<Category> AddCategory(Category category)
        {
            await Task.Run(() => { Categories.Add(category); }) ;
            return category;
        }
        public async Task<Category> UpdateCategory(int categoryId, Category category)
        {
            Category categoryForUpdating = await Task.Run(() => { return Categories.Where(c => c.Id == categoryId).FirstOrDefault(); });
            categoryForUpdating.Name = category.Name;
            return categoryForUpdating;
        }
        public async Task DeleteCategory(Category category)
        {
            await Task.Run(() => { Categories.Remove(category); Items.RemoveAll(c => c.CategoryId == category.Id); });
        }

        public async Task<List<Item>> GetItems(int categoryId, ItemParameters itemParameters)
        {
            return await Task.Run(() => 
            { 
                return Items.Where(x => x.CategoryId == categoryId)
                .Skip((itemParameters.PageNumber - 1) * itemParameters.PageSize)
                .Take(itemParameters.PageSize)
                .ToList(); 
            });
        }
        public async Task<Item> GetItemById(int itemId)
        {
            return await Task.Run(() => { return Items.Where(c => c.Id == itemId).FirstOrDefault(); });
        }
        public async Task<Item> AddItem(Item item)
        {
            await Task.Run(() => { Items.Add(item); });
            return item;
        }
        public async Task<Item> UpdateItem(int itemId, Item item)
        {
            Item itemForUpdating = await Task.Run(() => { return Items.Where(c => c.Id == itemId).FirstOrDefault(); });
            itemForUpdating.Name = item.Name;
            return itemForUpdating;
        }
        public async Task DeleteItem(Item item)
        {
            await Task.Run(() => { Items.Remove(item); });            
        }
    }
}
