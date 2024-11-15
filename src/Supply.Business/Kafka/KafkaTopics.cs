using Microsoft.Extensions.DependencyInjection;

namespace Supply.Business.Kafka;

public class KafkaTopicsOutput : AbstractKafkaTopics {
    public string SupplierProducts { get; set; } = "SupplierProducts";
    public string Categories { get; set; } = "Categories";

    public override IEnumerable<string> GetTopics() => new List<string>() { SupplierProducts, Categories };

}