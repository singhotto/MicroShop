using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supply.Repository.Model
{
    public class Product
    {
        public string Product_Id { get; set; } = Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public string Image { get; set; } = "";
        public required string User_Id { get; set; }
        public required string Category_Id { get; set; }
        public List<ProductOrderList> ProductOrderList { get; set; }
        public Category Category { get; set; }
        public required Supplier Supplier { get; set; }
    }
}
