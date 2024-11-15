using Microsoft.Extensions.DependencyInjection;

namespace Order.Business.Kafka;

public class KafkaTopicsInput : AbstractKafkaTopics {

    public string Orders { get; set; } = "Orders";
    public string Products { get; set; } = "Products";
    public string Users { get; set; } = "Users";

    public override IEnumerable<string> GetTopics() => new List<string>() { Orders, Products, Users };

}
