
namespace Payment.Shared.Dto
{
    public class OrderDto
    {
        public string User_Id { get; set; }
        public decimal Amount { get; set; } = 0;
        public List<ProductDto> Products { get; set; } = new List<ProductDto> { };
    }
}
