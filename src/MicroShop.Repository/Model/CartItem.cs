using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroShop.Repository.Model
{
    public class CartItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Product_Id { get; set; }
        public string ProductName { get; set; } = string.Empty;

        public int Stock_Quantity { get; set; }

        public decimal Price { get; set; }

        public ApplicationUser User { get; set; }
    }
}
