namespace CatalogService.Models
{
    public class Item
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string? Name { get; set; }
    }
}
