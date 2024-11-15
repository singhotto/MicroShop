namespace Payment.Shared.Dto
{
    public class TransactionDto
    {
        public int Payment_Id { get; set; } = 0;
        public required string User_Id { get; set; } = String.Empty;
        public required String CardOwner { get; set; } = String.Empty;
        public required String CardNumber { get; set; } = String.Empty;
        public required int Expiry_month { get; set; } = 8;
        public required int Expiry_year { get; set; } = 2028;
        public DateTime ExecutionDate { get; set; } = DateTime.UtcNow;
        public string? Payment_Status { get; set; }
        public required decimal Amount { get; set; } = 0;
    }

    public class TransactionOrderInsertDto
    {
        public required string User_Id { get; set; } = String.Empty;
        public required String CardOwner { get; set; } = String.Empty;
        public required String CardNumber { get; set; } = String.Empty;
        public required int Expiry_month { get; set; } = 8;
        public required int Expiry_year { get; set; } = 2028;
        public required int CVV { get; set; } = 123;
        public required decimal Amount { get; set; } = 0;
        public required int Order_Id { get; set; } = 0;
        public List<ProductDto> Products { get; set; } = new List<ProductDto> { };
    }
}
