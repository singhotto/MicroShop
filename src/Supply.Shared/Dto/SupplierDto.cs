namespace Supply.Shared.Dto
{
    public class SupplierDto
    {
        public string User_Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<ProductDto>? Products { get; set; } = null;
    }

    public class SupplierInsertDto
    {
        public string User_Id { get; set; } = String.Empty;
        public string FirstName { get; set; } = String.Empty;
        public string LastName { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
    }
}
