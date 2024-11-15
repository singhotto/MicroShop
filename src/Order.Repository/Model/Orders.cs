using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Repository.Model
{
    public class Orders
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_Id { get; set; } 
        public string User_Id { get; set; }
        public string Tracking_Number { get; set; } = string.Empty;
        public string Order_Status { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public User User { get; set; }
        public List<OrderProductList> Products { get; set; }
    }
}
