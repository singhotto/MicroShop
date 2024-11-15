using Payment.Shared.Dto;

namespace Payment.Business
{
    public interface IBusiness
    {
        public TransactionDto AddTransaction(TransactionOrderInsertDto transaction);
        public TransactionDto? GetTransaction(int  transactionId);
        public List<TransactionDto> GetTransactions();
        public List<TransactionDto> GetAllTransactions(string user_id = "", DateTime from = default, DateTime to = default);
    }
}
