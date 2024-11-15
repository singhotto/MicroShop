using System.Text.Json;
using Warehouse.Repository.Model;
using Warehouse.Shared.Dto;
using Utility.Kafka.Constants;
using Utility.Kafka.Messages;

namespace Warehouse.Business.Factory;

public static class TransactionalOutboxFactory
{

    #region Payment

    public static TransactionalOutbox CreateInsert(ProductDto dto) => Create(dto, Operations.Insert);

    public static TransactionalOutbox CreateUpdate(ProductDto dto) => Create(dto, Operations.Update);

    public static TransactionalOutbox CreateDelete(ProductDto dto) => Create(dto, Operations.Delete);

    private static TransactionalOutbox Create(ProductDto dto, string operation) => Create(nameof(ProductDto), dto, operation);

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
