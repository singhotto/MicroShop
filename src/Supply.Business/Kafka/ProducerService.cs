using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Supply.Repository;
using Supply.Repository.Model;
using Supply.Shared.Dto;
using Utility.Kafka.Abstractions.Clients;
using Utility.Kafka.Services;

namespace Supply.Business.Kafka;
public class ProducerService : ProducerService<KafkaTopicsOutput> {

    public ProducerService(ILogger<ProducerService> logger, IProducerClient producerClient, IAdministatorClient adminClient, IOptions<KafkaTopicsOutput> optionsTopics, IOptions<KafkaProducerServiceOptions> optionsProducerService, IServiceScopeFactory serviceScopeFactory) : base(logger, producerClient, adminClient, optionsTopics, optionsProducerService, serviceScopeFactory) {}

    protected override async Task OperationsAsync(CancellationToken cancellationToken) {
        using IServiceScope scope = ServiceScopeFactory.CreateScope();
        IRepository repository = scope.ServiceProvider.GetRequiredService<IRepository>();
        Logger.LogInformation("Acquisizione dei TransactionalOutbox da elaborare...");
        IEnumerable<TransactionalOutbox> transactionalOutboxList = repository.GetAllTransactionalOutbox().OrderBy(x => x.Id);
        if (!transactionalOutboxList.Any()) {
            Logger.LogInformation($"Non ci sono TransactionalOutbox da elaborare");
            return;
        }

        Logger.LogInformation("Ci sono {Count} TransactionalOutbox da elaborare", transactionalOutboxList.Count());

        foreach (TransactionalOutbox tran in transactionalOutboxList) {
            string groupMsg = $"del record {nameof(TransactionalOutbox)} con " +
                    $"{nameof(TransactionalOutbox.Id)} = {tran.Id}, " +
                    $"{nameof(TransactionalOutbox.Tabella)} = '{tran.Tabella}' e " +
                    $"{nameof(TransactionalOutbox.Messaggio)} = '{tran.Messaggio}'";

            Logger.LogInformation("Elaborazione {groupMsg}...", groupMsg);

            try {

                Logger.LogInformation("Determinazione del topic...");
                string topic = tran.Tabella switch
                {
                    nameof(CategoryDto) => KafkaTopics.Categories,
                    nameof(KafkaProductInsertDto) => KafkaTopics.SupplierProducts,
                    _ => throw new ArgumentOutOfRangeException($"La tabella {tran.Tabella} non è prevista come topic nel Producer")
                };
                Console.WriteLine("Topic Supply: " + topic);
                for (int i = 0; i<15; i++)
                {
                    Console.WriteLine("Desi");
                }
                Logger.LogInformation("Scrittura del messaggio Kafka sul topic '{topic}'...", topic);
                await ProducerClient.ProduceAsync(topic, tran.Messaggio, cancellationToken);

                Logger.LogInformation("Eliminazione {groupMsg}...", groupMsg);
                repository.DeleteTransactionalOutbox(tran.Id);

                repository.SaveChanges();
                Logger.LogInformation("Record eliminato");

            } catch (Exception ex) {
                Logger.LogError(ex, "Si è verificata un'eccezione nel metodo ProducerService.OperationsAsync durante l'elaborazione {groupMsg}: {ex}", groupMsg, ex);
            }
        }
    }
}
