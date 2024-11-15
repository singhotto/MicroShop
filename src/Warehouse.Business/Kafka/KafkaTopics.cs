using Microsoft.Extensions.DependencyInjection;

namespace Warehouse.Business.Kafka;

public class KafkaTopics : AbstractKafkaTopics {
    public string Products { get; set; } = "Products";
    public string SupplierProducts { get; set; } = "SupplierProducts";
    public string Categories { get; set; } = "Categories";
    public override IEnumerable<string> GetTopics() => new List<string>() { Products, SupplierProducts, Categories };

}