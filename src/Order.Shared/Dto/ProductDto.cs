namespace Order.Shared.Dto
{
    public class ProductDto
    {
        public string Product_Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
    }

    public class ProductInsertDto
    {
        public required string Name { get; set; } = string.Empty;
        public required string Description { get; set; } = string.Empty;
        public required decimal Price { get; set; } = 0;
    }

    public class KafkaProductInsertDto
    {
        public int Order_Id { get; set; }
        public string Product_Id { get; set; }
        public int Stock_Quantity { get; set; }
    }


    public class KafkaPaymentProductDto
    {
        public string Product_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = "";
        public int Stock_Quantity { get; set; }
        public string Category_Id { get; set; }
        public int Line_Number { get; set; }
        public int Floor_Number { get; set; }
    }
}
