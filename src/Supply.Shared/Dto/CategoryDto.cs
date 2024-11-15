namespace Supply.Shared.Dto
{
    public class CategoryDto
    {
        public string Category_Id { get; set; } = Guid.NewGuid().ToString();
        public string Category_Name { get; set; }
    }
}
