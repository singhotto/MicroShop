using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Utility.Kafka.Abstractions.MessageHandlers;
using Utility.Kafka.Services;

namespace Warehouse.Business.Kafka.MessageHandlers;

public class MessageHandlerFactory : IMessageHandlerFactory {
    private readonly ILogger<ConsumerService<KafkaTopics>> _logger;
    private readonly KafkaTopics _optionsTopics;

    public MessageHandlerFactory(ILogger<ConsumerService<KafkaTopics>> logger, IOptions<KafkaTopics> optionsTopics) {
        _logger = logger;
        _optionsTopics = optionsTopics.Value;
    }

    public IMessageHandler Create(string topic, IServiceProvider serviceProvider) {
        if (topic == _optionsTopics.SupplierProducts)
            return ActivatorUtilities.CreateInstance<SupplierProductsKafkaMessageHandler>(serviceProvider);
        if(topic == _optionsTopics.Categories)
            return ActivatorUtilities.CreateInstance<SupplierCateogiesKafkaMessageHandler>(serviceProvider);
        if (topic == "Products") return null;

        throw new ArgumentOutOfRangeException(nameof(topic), $"Il topic '{topic}' non è gestito");
    }
}