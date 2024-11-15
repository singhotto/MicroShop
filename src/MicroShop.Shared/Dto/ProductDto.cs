using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroShop.Shared.Dto
{
    public class ProductDto
    {
        public string Product_Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string Image { get; set; }
        public int CategoryId { get; set; } = 0;
    }

}
