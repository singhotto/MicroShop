using MicroShop.Repository.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MicroShop.Repository
{
    public class Repository : IRepository
    {
        private readonly MicroShopDbContext _microShopDbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        public Repository(MicroShopDbContext microShopDbContext, UserManager<ApplicationUser> userManager) {
            _microShopDbContext = microShopDbContext;
            _userManager = userManager;

        }

        public void SaveChanges() => _microShopDbContext.SaveChanges();

        public IDbContextTransaction BeginTRansaction() => _microShopDbContext.Database.BeginTransaction();

        public void CreateTransaction(Action action) {
            if (_microShopDbContext.Database.CurrentTransaction != null) {
                // La connessione è già in una transazione
                action();
            } else {
                // Viene avviata una transazione 
                using IDbContextTransaction transaction = _microShopDbContext.Database.BeginTransaction();
                try {
                    action();
                    transaction.Commit();
                } catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public CartItem Insert(CartItem item)
        {
            _microShopDbContext.Cart.Add(item);
            return item;
        }

        public IQueryable<ApplicationUser> ReadUser()
        {

            return _microShopDbContext.Users;
        }

        public IQueryable<CartItem> ReadCart()
        {
            return _microShopDbContext.Cart; 
        }

        public void Delete(CartItem item)
        {
            _microShopDbContext.Cart.Remove(item);
        }

        public void DeleteRange(List<CartItem> items)
        {
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("items length: ", items.Count);
            }

            _microShopDbContext.Cart.RemoveRange(items);
        }

        public async Task<IdentityResult> MakeSupplierAsync(ApplicationUser user)
        {
            return await _userManager.AddToRoleAsync(user, "Supplier");
        }

        #region TransactionalOutbox


        public IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox() => _microShopDbContext.TransactionalOutboxList.ToList();

        public TransactionalOutbox? GetTransactionalOutboxByKey(long id)
        {

            return _microShopDbContext.TransactionalOutboxList.FirstOrDefault(x =>
                x.Id == id);
        }

        public void DeleteTransactionalOutbox(long id)
        {

            _microShopDbContext.TransactionalOutboxList.Remove(
                GetTransactionalOutboxByKey(id) ??
                throw new ArgumentException($"TransactionalOutbox con id {id} non trovato", nameof(id)));
        }

        public void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox)
        {
            _microShopDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        }

        #endregion
    }
}
