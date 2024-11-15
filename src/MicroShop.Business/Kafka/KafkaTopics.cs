using Microsoft.Extensions.DependencyInjection;

namespace MicroShop.Business.Kafka;

public class KafkaTopicsOutput : AbstractKafkaTopics {
    public string Users { get; set; } = "Users";

    public override IEnumerable<string> GetTopics() => new List<string>() { Users };

}