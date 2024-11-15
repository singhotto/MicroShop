namespace MicroShop.Shared.Dto
{
    public class CartDto
    {
        public int Id { get; set; }

        public string Product_Id { get; set; }
        public string ProductName { get; set; }

        public int Stock_Quantity { get; set; }

        public decimal Price { get; set; }
    }

    public class CartInsertDto
    {
        public string UserName { get; set; } = string.Empty;

        public string Product_Id { get; set; }
        public string ProductName {  get; set; }

        public int Stock_Quantity { get; set; } = 0;

        public decimal Price { get; set; } = 0;
    }
}
