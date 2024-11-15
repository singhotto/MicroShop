using System.Text.Json;
using MicroShop.Repository.Model;
using MicroShop.Shared.Dto;
using Utility.Kafka.Constants;
using Utility.Kafka.Messages;

namespace MicroShop.Business.Factory;

public static class TransactionalOutboxFactory
{

    #region Payment

    public static TransactionalOutbox CreateInsert(UserDto dto) => Create(dto, Operations.Insert);

    public static TransactionalOutbox CreateUpdate(UserDto dto) => Create(dto, Operations.Update);

    public static TransactionalOutbox CreateDelete(UserDto dto) => Create(dto, Operations.Delete);

    private static TransactionalOutbox Create(UserDto dto, string operation) => Create(nameof(UserDto), dto, operation);

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
