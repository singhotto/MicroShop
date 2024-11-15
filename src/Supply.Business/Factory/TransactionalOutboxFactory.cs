using System.Text.Json;
using Supply.Repository.Model;
using Supply.Shared.Dto;
using Utility.Kafka.Constants;
using Utility.Kafka.Messages;

namespace Supply.Business.Factory;

public static class TransactionalOutboxFactory
{

    #region SupplyProduct

    public static TransactionalOutbox CreateInsert(KafkaProductInsertDto dto) => Create(dto, Operations.Insert);

    public static TransactionalOutbox CreateUpdate(KafkaProductInsertDto dto) => Create(dto, Operations.Update);

    public static TransactionalOutbox CreateDelete(KafkaProductInsertDto dto) => Create(dto, Operations.Delete);

    private static TransactionalOutbox Create(KafkaProductInsertDto dto, string operation) => Create(nameof(KafkaProductInsertDto), dto, operation);

    #endregion


    #region SupplyCategory

    public static TransactionalOutbox CreateInsert(CategoryDto dto) => Create(dto, Operations.Insert);

    public static TransactionalOutbox CreateUpdate(CategoryDto dto) => Create(dto, Operations.Update);

    public static TransactionalOutbox CreateDelete(CategoryDto dto) => Create(dto, Operations.Delete);

    private static TransactionalOutbox Create(CategoryDto dto, string operation) => Create(nameof(CategoryDto), dto, operation);

    #endregion

    private static TransactionalOutbox Create<TDTO>(string table, TDTO dto, string operation) where TDTO : class, new()
    {

        OperationMessage<TDTO> opMsg = new OperationMessage<TDTO>() {
            Dto = dto,
            Operation = operation
        };
        opMsg.CheckMessage();

        return new TransactionalOutbox(){
            Tabella = table,
            Messaggio = JsonSerializer.Serialize(opMsg)
        };
    }
}
