using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.Repository.Model;

namespace Warehouse.Repository
{
    public class Repository : IRepository
    {
        private readonly WarehouseDbContext _warehouseDbContext;
        public Repository(WarehouseDbContext uniprExampleDbContext) {
            _warehouseDbContext = uniprExampleDbContext;
        }
        public void SaveChanges() => _warehouseDbContext.SaveChanges();
        public IDbContextTransaction BeginTRansaction() => _warehouseDbContext.Database.BeginTransaction();
        public void CreateTransaction(Action action) {
            if (_warehouseDbContext.Database.CurrentTransaction != null) {
                // La connessione è già in una transazione
                action();
            } else {
                // Viene avviata una transazione 
                using IDbContextTransaction transaction = _warehouseDbContext.Database.BeginTransaction();
                try {
                    action();
                    transaction.Commit();
                } catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public IQueryable<Category> ReadCategory()
        {
            return _warehouseDbContext.Category;
        }
        public Category? GetCategoryById(string id)
        {
            return _warehouseDbContext.Category.SingleOrDefault(x => x.Category_Id == id);
        }
        public Category? UpdateCategory(string id, Category newCategory)
        {
            Category? c = _warehouseDbContext.Category.SingleOrDefault(x => x.Category_Id == id);

            if (c != null)
                c.Category_Name = newCategory.Category_Name;

            _warehouseDbContext.SaveChanges();
            return c;
        }
        public void DeleteCategory(string id)
        {
            Category? c = _warehouseDbContext.Category.SingleOrDefault(x => x.Category_Id == id);

            if (c != null)
                _warehouseDbContext.Category.Remove(c);
        }
        public IQueryable<Product> ReadProduct()
        {
            return _warehouseDbContext.Product;
        }
        public Product? GetProductById(string id)
        {
            return _warehouseDbContext.Product.SingleOrDefault(x => x.Product_Id == id);
        }
        public Product? UpdateProduct(string id, Product newProduct)
        {
            Product? product = _warehouseDbContext.Product.SingleOrDefault(x => x.Product_Id == id);

            if (product == null) return null;

            product.Name = newProduct.Name;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;
            product.Category_Id = newProduct.Category_Id;
            product.Stock_Quantity = newProduct.Stock_Quantity;

            _warehouseDbContext.SaveChanges();

            return product;
        }
       public  void DeleteProduct(string id)
        {
            Product? product = _warehouseDbContext.Product.SingleOrDefault(x => x.Product_Id == id);

            if (product != null)_warehouseDbContext.Product.Remove(product);
        }
        public void Insert(Category category)
        {
            if (_warehouseDbContext.Category.Any(c => c.Category_Name == category.Category_Name))
                return;
            _warehouseDbContext.Category.Add(category);
        }
        public void Insert(Product product)
        {
            if (_warehouseDbContext.Product.Any(c => (c.Name == product.Name && c.Price == product.Price)))
                return;
            _warehouseDbContext.Product.Add(product);
        }

        #region TransactionalOutbox
        public IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox() => _warehouseDbContext.TransactionalOutboxList.ToList();

        public TransactionalOutbox? GetTransactionalOutboxByKey(long id)
        {

            return _warehouseDbContext.TransactionalOutboxList.FirstOrDefault(x =>
                x.Id == id);
        }

        public void DeleteTransactionalOutbox(long id)
        {

            _warehouseDbContext.TransactionalOutboxList.Remove(
                GetTransactionalOutboxByKey(id) ??
                throw new ArgumentException($"TransactionalOutbox con id {id} non trovato", nameof(id)));
        }

        public void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox)
        {
            _warehouseDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        }

        #endregion
    }
}
