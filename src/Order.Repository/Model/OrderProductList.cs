using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Repository.Model
{
    public class OrderProductList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Order_Product_Id { get; set; }
        public required int Order_Id { get; set; }
        public required string Product_Id { get; set; }
        public required int Stock_Quantity { get; set; }

        [ForeignKey("Product_Id")]
        public Product Product { get; set; }

        [ForeignKey("Order_Id")]
        public Orders Order { get; set; }
    }
}
