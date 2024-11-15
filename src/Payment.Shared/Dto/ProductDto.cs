namespace Payment.Shared.Dto
{
    public class ProductDto
    {
        public string Product_Id { get; set; }
        public decimal Price { get; set; } = 0;
        public int Stock_Quantity { get; set; } = 0;
    }
}
