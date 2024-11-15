namespace Supply.Shared.Dto
{
    public class OrderDto
    {
        public int Order_Id { get; set; }
        public string User_Id { get; set; } 
        public DateTime Date { get; set; }
        public string Status { get; set; } 
        public string? Tracking_Number { get; set; }
        public List<ProductOrderDto>? Products { get; set; }
        public SupplierDto? Supplier { get; set; }
    }
    public class OrderProductDto
    {
        public int Order_Id { get; set; }
        public required string User_Id { get; set; } = String.Empty;
        public required DateTime Date { get; set; } = new DateTime();
        public required string Status { get; set; } = string.Empty;
        public string? Tracking_Number { get; set; } = string.Empty;
        public List<ProductDto>? Products { get; set; } = null;
    }

    public class OrderProductInsertDto
    {
        public string Product_Id { get; set; }
        public int Stock_Quantity { get; set; }
    }
}
