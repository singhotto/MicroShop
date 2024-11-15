using System.ComponentModel.DataAnnotations.Schema;

namespace Supply.Repository.Model
{
    public class Supplier
    {
        public required string User_Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Address { get; set; } 
        public required List<Product> Products { get; set; }
        public List<Order> Orders { get; set; }
    }
}
