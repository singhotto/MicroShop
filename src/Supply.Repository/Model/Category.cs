namespace Supply.Repository.Model
{
    public class Category
    {
        public string Category_Id { get; set; } = Guid.NewGuid().ToString();
        public string Category_Name { get; set;}
        public List<Product> Products { get; set;} 
    }
}
