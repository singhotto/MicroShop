namespace Supply.Shared.Dto
{
    public class ProductOrderDto
    {
        public int Product_Order_Id { get; set; } = 0;

        public int? Order_Id { get; set; } = null;

        public int? Product_Id { get; set; } = null;

        public int Stock_Quantity { get; set; } = 0;

        public ProductDto? Product { get; set; } = null;

    }
    public class ProductOrderInsertDto
    {
        public int? Product_Id { get; set; } = null;

        public int Stock_Quantity { get; set; } = 0;
    }
}
