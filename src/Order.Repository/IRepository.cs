using Microsoft.EntityFrameworkCore.Storage;
using Order.Repository.Model;

namespace Order.Repository {
     public interface IRepository {
        public void SaveChanges();

        public IDbContextTransaction BeginTRansaction();

        public void CreateTransaction(Action action);

        #region Product
        public void Insert(Product product);
        public IQueryable<Product> ReadProduct();
        public Product? GetProductById(string id);
        public Product UpdateProduct(string productId, Product newProduct);
        public void DeleteProduct(string id);
        void Delete(Product product);
        #endregion


        #region Order
        public void Insert(Orders order);
        public void InsertProductsToOrder(List<OrderProductList> products);
        public IQueryable<Orders> ReadOrder();
        public Orders? GetOrderById(int id);
        public Orders? UpdateOrder(int order_id, Orders newOrder);
        public Orders? UpdateOrderStatus(int orderId, string newStatus);
        public void DeleteOrder(int id);
        #endregion

        #region User
        public void Insert(User user);
        #endregion User


        #region TransactionalOutbox

        //IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox();

        //TransactionalOutbox? GetTransactionalOutboxByKey(long id);

        //void DeleteTransactionalOutbox(long id);

        //void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox);

        #endregion

    }
}
