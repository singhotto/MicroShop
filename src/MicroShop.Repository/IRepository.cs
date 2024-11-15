using Microsoft.EntityFrameworkCore.Storage;
using MicroShop.Repository.Model;
using Microsoft.AspNetCore.Identity;

namespace MicroShop.Repository {
     public interface IRepository {
        public void SaveChanges();

        public IDbContextTransaction BeginTRansaction();

        public void CreateTransaction(Action action);

        public CartItem Insert(CartItem item);
        public IQueryable<ApplicationUser> ReadUser();

        public IQueryable<CartItem> ReadCart();

        public void Delete(CartItem item);

        public void DeleteRange(List<CartItem> items);

        public Task<IdentityResult> MakeSupplierAsync(ApplicationUser user);

        #region TransactionalOutbox

        IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox();

        TransactionalOutbox? GetTransactionalOutboxByKey(long id);

        void DeleteTransactionalOutbox(long id);

        void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox);

        #endregion

    }
}
