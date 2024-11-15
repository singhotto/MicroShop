namespace Supply.Shared.Dto
{
    public class ProductDto
    {
        public string Product_Id { get; set; }
        public string Name { get; set; } 
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Category_Id { get; set; }
    }

    public class ProductInsertAutorized
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; } 
        public string Image {  get; set; } = "";
        public required string Category{ get; set;}
    }
    public class ProductInsertDto
    {
        public required string Name { get; set; } 
        public required string Description { get; set; } 
        public required decimal Price { get; set; }
        public string Image { get; set; } = "";
        public required string Supplier_Id { get; set; }
        public required string Category_Id { get; set; }
    }

    public class KafkaProductInsertDto
    {
        public string Product_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = "";
        public int Stock_Quantity { get; set; }
        public string Category_Id { get; set; }
    }
}
