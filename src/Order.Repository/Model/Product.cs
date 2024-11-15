using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Repository.Model
{
    public class Product
    {
        public string Product_Id { get; set; }
        public string Name { get; set;}
        public string Description { get; set;}
        public decimal Price { get; set;}
        public List<OrderProductList> Orders { get; set;}
    }
}
