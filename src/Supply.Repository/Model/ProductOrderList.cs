using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Supply.Repository.Model
{
    public class ProductOrderList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_Order_Id { get; set; }
        public required int Order_Id { get; set; }
        public required string Product_Id {  get; set; }
        public required int Stock_Quantity { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
