using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supply.Repository.Model
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_Id { get; set; }
        public required string User_Id { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = "Order Created";
        public string? Tracking_Number { get; set; } = "Pending";
        public List<ProductOrderList>  Products { get; set; }
        public Supplier Supplier { get; set; }
    }
}
