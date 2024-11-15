namespace Order.Shared.Dto
{
    public class OrderDto
    {
        public int Order_Id { get; set; }
        public string User_Id { get; set; }
        public string User_Adress { get; set; }
        public string Tracking_Number { get; set; }
        public string Order_Status { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public List<OrderProductDto> Products { get; set; }
    }

    public class KafkaOrderInsertDto
    {
        public string User_Id { get; set; }
        public string Tracking_Number { get; set; } = string.Empty;
        public string Order_Status { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
        public DateTime Created_At { get; set; } = DateTime.Now;
        public List<KafkaProductInsertDto> Products { get; set; }
    }
    public class OrderInsertDto
    {
        public string User_Id { get; set; } = string.Empty;
        public required string Status { get; set; } = "Order Created";
        public List<ProductOrderInsertDto>? ProductOrders { get; set; } = null;
    }

    public class OrderProductDto
    {
        public int Order_Id { get; set; }
        public string User_Id { get; set; }
        public string Tracking_Number { get; set; }
        public string Order_Status { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public List<ProductDto>? Products { get; set; } = null;
    }

    public class OrderInfoDto
    {
        public int Order_Id { get; set; }
        public string User_Name { get; set; }
        public string User_Address { get; set; }
        public string Tracking_Number { get; set; }
        public string Order_Status { get; set; }
        public decimal Amount { get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public List<ProductDto>? Products { get; set; } = null;
    }
}
