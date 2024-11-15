using Microsoft.EntityFrameworkCore.Storage;
using Warehouse.Repository.Model;

namespace Warehouse.Repository {
     public interface IRepository {
        public void SaveChanges();

        public IDbContextTransaction BeginTRansaction();

        public void CreateTransaction(Action action);

        #region Category

        public void Insert(Category category);
        public IQueryable<Category> ReadCategory();
        public Category? GetCategoryById(string id);
        public Category? UpdateCategory(string id, Category newCategory);

        public void DeleteCategory(string id);

        #endregion

        #region Product

        public void Insert(Product product);
        public  IQueryable<Product> ReadProduct();
        public  Product? GetProductById(string id);
        public Product? UpdateProduct(string id, Product newProduct);
        public void DeleteProduct(string id);

        #endregion

        #region TransactionalOutbox

        IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox();

        TransactionalOutbox? GetTransactionalOutboxByKey(long id);

        void DeleteTransactionalOutbox(long id);

        void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox);

        #endregion

    }
}
