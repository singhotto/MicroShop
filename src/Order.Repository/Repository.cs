using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Order.Repository.Model;

namespace Order.Repository {
    public class Repository : IRepository
    {
        private readonly OrderDbContext _orderDbContext;
        public Repository(OrderDbContext orderDbContext) {
            _orderDbContext = orderDbContext;

        }

        public void SaveChanges() => _orderDbContext.SaveChanges();

        public IDbContextTransaction BeginTRansaction() => _orderDbContext.Database.BeginTransaction();

        public void CreateTransaction(Action action) {
            if (_orderDbContext.Database.CurrentTransaction != null) {
                // La connessione è già in una transazione
                action();
            } else {
                // Viene avviata una transazione 
                using IDbContextTransaction transaction = _orderDbContext.Database.BeginTransaction();
                try {
                    action();
                    transaction.Commit();
                } catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Insert(Product product)
        {
            if (_orderDbContext.Product.Any(p => (p.Name == product.Name && p.Price == product.Price)))
                return;
            _orderDbContext.Product.Add(product);
        }

        public IQueryable<Product> ReadProduct()
        {
            return _orderDbContext.Product;
        }

        public Product? GetProductById(string id)
        {
            return _orderDbContext.Product.FirstOrDefault(x => x.Product_Id == id);
        }

        public Product UpdateProduct(string productId, Product newProduct)
        {
            Product? product = _orderDbContext.Product.FirstOrDefault(x => x.Product_Id == productId);

            if (product == null)
                throw new Exception("");

            product.Price = newProduct.Price;
            product.Description = newProduct.Description;
            product.Name = newProduct.Name;

            _orderDbContext.SaveChanges();

            return product;
        }

        public void DeleteProduct(string id)
        {
            Product? product = _orderDbContext.Product.FirstOrDefault(x => x.Product_Id == id);

            if (product != null)
                _orderDbContext.Product.Remove(product);
        }

        public void Delete(Product product)
        {
            _orderDbContext.Product.Remove(product);
        }

        public void Insert(Orders order)
        {
            _orderDbContext.Order.Add(order);
        }

        public IQueryable<Orders> ReadOrder()
        {
            return _orderDbContext.Order;
        }

        public Orders? GetOrderById(int id)
        {
            return _orderDbContext.Order.FirstOrDefault(x => x.Order_Id == id);
        }

        public Orders? UpdateOrder(int order_id, Orders newOrder)
        {
            Orders? order = _orderDbContext.Order.FirstOrDefault(x => x.Order_Id == order_id);

            if (order == null) return null;

            order.User_Id = newOrder.User_Id;
            order.Created_At = newOrder.Created_At;
            order.Order_Status = newOrder.Order_Status;
            order.Tracking_Number = newOrder.Tracking_Number;
            order.Products = newOrder.Products;

            _orderDbContext.SaveChanges();

            return order;
        }

        public Orders? UpdateOrderStatus(int orderId, string newStatus)
        {
            Orders? order = _orderDbContext.Order.FirstOrDefault(x => x.Order_Id == orderId);

            if (order == null)
                return null;

            order.Order_Status = newStatus;

            _orderDbContext.SaveChanges();

            return order;
        }

        public void DeleteOrder(int id)
        {
            Orders? order = _orderDbContext.Order.FirstOrDefault(x => x.Order_Id == id);

            if (order != null)
                _orderDbContext.Order.Remove(order);
        }

        public void Insert(User user)
        {
            if (_orderDbContext.User.Any(u => u.User_Id == user.User_Id))
                return;
            _orderDbContext.User.Add(user);
        }

        public void InsertProductsToOrder(List<OrderProductList> products)
        {
            if (products == null)
                throw new Exception("products == null");
            _orderDbContext.OrderProductList.AddRange(products);
        }


        #region TransactionalOutbox


        //public IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox() => _warehouseDbContext.TransactionalOutboxList.ToList();

        //public TransactionalOutbox? GetTransactionalOutboxByKey(long id)
        //{

        //    return _warehouseDbContext.TransactionalOutboxList.FirstOrDefault(x =>
        //        x.Id == id);
        //}

        //public void DeleteTransactionalOutbox(long id)
        //{

        //    _warehouseDbContext.TransactionalOutboxList.Remove(
        //        GetTransactionalOutboxByKey(id) ??
        //        throw new ArgumentException($"TransactionalOutbox con id {id} non trovato", nameof(id)));
        //}

        //public void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox)
        //{
        //    _warehouseDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        //}

        #endregion
    }
}
