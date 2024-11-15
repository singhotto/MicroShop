using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Warehouse.Shared.Dto
{
    public class CategoryDto
    {
        public string Category_Id { get; set; } 
        public string Category_Name { get; set; }
        public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    }

    public class CategoryInsertDto
    {
        public string Category_Name { get; set; }
    }
}
