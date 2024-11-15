using Microsoft.Extensions.DependencyInjection;

namespace Payment.Business.Kafka;

public class KafkaTopicsOutput : AbstractKafkaTopics {
    public string Orders { get; set; } = "Orders";

    public override IEnumerable<string> GetTopics() => new List<string>() { Orders };

}