using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouse.Repository.Model
{
    public class Product
    {
        public string Product_Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public string Image { get; set; } = "";
        public required int Stock_Quantity { get; set; }
        public required string Category_Id { get; set; }
        public int Line_Number { get; set; } = 0;
        public int Floor_Number { get; set; } = 0;
        public Category? Category { get; set; }
    }
}
