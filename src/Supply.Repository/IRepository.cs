using Microsoft.EntityFrameworkCore.Storage;
using Supply.Repository.Model;

namespace Supply.Repository {
     public interface IRepository {
        public void SaveChanges();

        public IDbContextTransaction BeginTRansaction();

        public void CreateTransaction(Action action);

        #region Supplier
        public void Insert(Supplier supplier);
        public IQueryable<Supplier> ReadSupplier();
        public Supplier? GetSupplierById(string id);
        public Supplier UpdateSupplier(string supplierId, Supplier newSupplier);
        public void DeleteSupplier(string id);
        void Delete(Supplier supplier);
        #endregion

        #region Product
        public void Insert(Product product);
        public void Insert(List<ProductOrderList> products);
        public IQueryable<Product> ReadProduct();
        public Boolean isAlready(Product product);
        public Product? GetProductById(string id);
        public Product UpdateProduct(string productId, Product newProduct);
        public void DeleteProduct(string id);
        void Delete(Product product);
        #endregion

        #region Category

        public void Insert(Category category);
        public Boolean isAlready(Category product);
        public IQueryable<Category> ReadCategory();

        #endregion Category


        #region Order
        public void Insert(Order order);
        public IQueryable<Order> ReadOrder();
        public Order? GetOrderById(int id);
        public Order? UpdateOrder(int order_id, Order newOrder);
        public Order? UpdateOrderStatus(int orderId, string newStatus);
        public void DeleteOrder(int id);
        void Delete(Order order);
        #endregion

        #region TransactionalOutbox

        IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox();

        TransactionalOutbox? GetTransactionalOutboxByKey(long id);

        void DeleteTransactionalOutbox(long id);

        void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox);

        #endregion

    }
}
