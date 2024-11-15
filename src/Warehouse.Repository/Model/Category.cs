using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Repository.Model
{
    public record Category
    {
        public string Category_Id { get; set; } = Guid.NewGuid().ToString();
        public string Category_Name { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
