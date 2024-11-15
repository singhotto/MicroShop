using Microsoft.AspNetCore.Identity;

namespace MicroShop.Repository.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address {  get; set; }
        public List<CartItem> CartItems { get; set; }
    }
}
