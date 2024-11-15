namespace Order.Repository.Model
{
    public class User
    {
        public string User_Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public List<Orders> Orders { get; set; } = new List<Orders>();
    }
}
