using Microsoft.EntityFrameworkCore.Storage;
using Payment.Repository.Model;

namespace Payment.Repository {
     public interface IRepository {
        public void SaveChanges();

        public IDbContextTransaction BeginTRansaction();

        public void CreateTransaction(Action action);

        public Transaction Insert(Transaction payment);

        public IQueryable<Transaction> ReadTrancations();

        public Transaction? GetTransaction(int id);
        IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox();
        void DeleteTransactionalOutbox(long id);
        TransactionalOutbox? GetTransactionalOutboxByKey(long id);
        void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox);
    }
}
