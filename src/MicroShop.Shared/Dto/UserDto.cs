using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroShop.Shared.Dto
{
    public class UserDto
    {
        public string User_Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Address { get; set; }
    }

    public class SupplierDto
    {
        public string supplierEmail { get; set; }
    }

}