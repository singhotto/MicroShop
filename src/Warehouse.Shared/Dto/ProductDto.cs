using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Shared.Dto
{
    public class ProductDto
    {
        public string Product_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; } = "";
        public int Stock_Quantity { get; set; }
        public string Category_Id { get; set; }
        public int Line_Number {  get; set; }
        public int Floor_Number { get; set; }
    }

    public class ProductInsertDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public string Image { get; set; } = "";
        public required int Stock_Quantity { get; set; }
        public required string Category_Id { get; set; }
        public int Line_Number { get; set; } = 0;
        public int Floor_Number { get; set; } = 0;
    }
}
