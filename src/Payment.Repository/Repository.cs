using Microsoft.EntityFrameworkCore.Storage;
using Payment.Repository.Model;

namespace Payment.Repository {
    public class Repository : IRepository
    {
        private readonly PaymentDbContext _paymentDbContext;
        public Repository(PaymentDbContext paymentDbContext) {
            _paymentDbContext = paymentDbContext;
        }

        public void SaveChanges() => _paymentDbContext.SaveChanges();

        public IDbContextTransaction BeginTRansaction() => _paymentDbContext.Database.BeginTransaction();

        public void CreateTransaction(Action action) {
            if (_paymentDbContext.Database.CurrentTransaction != null) {
                // La connessione è già in una transazione
                action();
            } else {
                // Viene avviata una transazione 
                using IDbContextTransaction transaction = _paymentDbContext.Database.BeginTransaction();
                try {
                    action();
                    transaction.Commit();
                } catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public Transaction Insert(Transaction payment)
        {
            _paymentDbContext.Transaction.Add(payment);

            return payment;
        }

        public IQueryable<Transaction> ReadTrancations()
        {
            return _paymentDbContext.Transaction;
        }

        public Transaction? GetTransaction(int id)
        {
            return _paymentDbContext.Transaction.FirstOrDefault(x => x.Payment_Id == id);
        }



        #region TransactionalOutbox


        public IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox() => _paymentDbContext.TransactionalOutboxList.ToList();

        public TransactionalOutbox? GetTransactionalOutboxByKey(long id)
        {

            return _paymentDbContext.TransactionalOutboxList.FirstOrDefault(x =>
                x.Id == id);
        }


        public void DeleteTransactionalOutbox(long id)
        {

            _paymentDbContext.TransactionalOutboxList.Remove(
                GetTransactionalOutboxByKey(id) ??
                throw new ArgumentException($"TransactionalOutbox con id {id} non trovato", nameof(id)));
        }

        public void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox)
        {
            _paymentDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        }

        #endregion
    }
}
