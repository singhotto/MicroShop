using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Supply.Repository;
using Supply.Repository.Model;
using static Confluent.Kafka.ConfigPropertyNames;

namespace Supply.Repository {
    public class Repository : IRepository
    {
        private readonly SupplyDbContext _supplyDbContext;
        public Repository(SupplyDbContext uniprExampleDbContext) {
            _supplyDbContext = uniprExampleDbContext;

        }

        public void SaveChanges() => _supplyDbContext.SaveChanges();

        public IDbContextTransaction BeginTRansaction() => _supplyDbContext.Database.BeginTransaction();

        public void CreateTransaction(Action action) {
            if (_supplyDbContext.Database.CurrentTransaction != null) {
                // La connessione è già in una transazione
                action();
            } else {
                // Viene avviata una transazione 
                using IDbContextTransaction transaction = _supplyDbContext.Database.BeginTransaction();
                try {
                    action();
                    transaction.Commit();
                } catch (Exception) {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public void Insert(Supplier supplier)
        {
            if (_supplyDbContext.Supplier.Any(s => s.User_Id == supplier.User_Id))
                return;
            _supplyDbContext.Supplier.Add(supplier);
        }

        public IQueryable<Supplier> ReadSupplier()
        {
            return _supplyDbContext.Supplier;
        }

        public Supplier? GetSupplierById(string id)
        {
            return _supplyDbContext.Supplier.FirstOrDefault(x => x.User_Id == id);
        }

        public Supplier UpdateSupplier(string supplierId, Supplier newSupplier)
        {
            Supplier? s = _supplyDbContext.Supplier.FirstOrDefault(x => x.User_Id == supplierId);

            if (s == null)
            {
                throw new Exception("");
            }

            s.FirstName = newSupplier.FirstName;
            s.LastName  = newSupplier.LastName;
            s.Address = newSupplier.Address;  

            _supplyDbContext.SaveChanges();

            return s;
        }

        public void DeleteSupplier(string id)
        {
            Supplier? existingSupplier = _supplyDbContext.Supplier
                .FirstOrDefault(s => s.User_Id == id);

            if (existingSupplier == null)
            {
                throw new Exception("Fornitore non trovato");
            }

            _supplyDbContext.Supplier.Remove(existingSupplier);
        }

        public void Delete(Supplier supplier)
        {
            _supplyDbContext.Supplier.Remove(supplier);
        }

        public void Insert(Product product)
        {
            if (_supplyDbContext.Product.Any(s => (s.Name == product.Name && s.Price == product.Price)))
                return;
            _supplyDbContext.Product.Add(product);
        }

        public IQueryable<Product> ReadProduct()
        {
            return _supplyDbContext.Product;
        }

        public Product? GetProductById(string id)
        {
            return _supplyDbContext.Product.FirstOrDefault(x => x.Product_Id == id);
        }

        public Product UpdateProduct(string productId, Product newProduct)
        {
            Product? product = _supplyDbContext.Product.FirstOrDefault(x => x.Product_Id == productId);

            if (product == null)
                throw new Exception("");

            product.Price = newProduct.Price;
            product.Description = newProduct.Description;
            product.Supplier = newProduct.Supplier;
            product.Name = newProduct.Name;

            _supplyDbContext.SaveChanges();

            return product;
        }

        public void DeleteProduct(string id)
        {
            Product? product = _supplyDbContext.Product.FirstOrDefault(x => x.Product_Id == id);

            if (product != null)
                _supplyDbContext.Product.Remove(product);
        }

        public void Delete(Product product)
        {
            _supplyDbContext.Product.Remove(product);
        }

        public void Insert(Order order)
        {
            _supplyDbContext.Order.Add(order);
        }

        public IQueryable<Order> ReadOrder()
        {
            return _supplyDbContext.Order;
        }

        public Order? GetOrderById(int id)
        {
            return _supplyDbContext.Order.FirstOrDefault(x => x.Order_Id == id);
        }

        public Order? UpdateOrder(int order_id, Order newOrder)
        {
            Order? order = _supplyDbContext.Order.FirstOrDefault(x => x.Order_Id == order_id);

            if (order == null) return null;

            order.User_Id = newOrder.User_Id;
            order.Date = newOrder.Date;
            order.Status = newOrder.Status;
            order.Tracking_Number = newOrder.Tracking_Number;
            order.Supplier = newOrder.Supplier;

            _supplyDbContext.SaveChanges();

            return order;
        }

        public Order? UpdateOrderStatus(int orderId, string newStatus)
        {
            Order? order = _supplyDbContext.Order.FirstOrDefault(x => x.Order_Id == orderId);

            if (order == null)
                return null;

            order.Status = newStatus;

            _supplyDbContext.SaveChanges();

            return order;
        }

        public void DeleteOrder(int id)
        {
            Order? order = _supplyDbContext.Order.FirstOrDefault(x => x.Order_Id == id);

            if (order != null)
                _supplyDbContext.Order.Remove(order);
        }

        public void Delete(Order order)
        {
            _supplyDbContext.Order.Remove(order);
        }

        public void Insert(Category category)
        {

            if (_supplyDbContext.Category.Any(s => s.Category_Name == category.Category_Name))
                return;
            _supplyDbContext.Category.Add(category);
        }

        public IQueryable<Category> ReadCategory()
        {
            return _supplyDbContext.Category;
        }

        public bool isAlready(Product product)
        {

            return _supplyDbContext.Product.Any(p => p.Name == product.Name);
        }

        public void Insert(List<ProductOrderList> products)
        {
            _supplyDbContext.ProductOrdersList.AddRange(products);
        }

        public bool isAlready(Category category)
        {
            return _supplyDbContext.Category.Any(c => c.Category_Name == category.Category_Name);
        }

        #region TransactionalOutbox
        public IEnumerable<TransactionalOutbox> GetAllTransactionalOutbox() => _supplyDbContext.TransactionalOutboxList.ToList();

        public TransactionalOutbox? GetTransactionalOutboxByKey(long id)
        {

            return _supplyDbContext.TransactionalOutboxList.FirstOrDefault(x =>
                x.Id == id);
        }


        public void DeleteTransactionalOutbox(long id)
        {

            _supplyDbContext.TransactionalOutboxList.Remove(
                GetTransactionalOutboxByKey(id) ??
                throw new ArgumentException($"TransactionalOutbox con id {id} non trovato", nameof(id)));
        }

        public void InsertTransactionalOutbox(TransactionalOutbox transactionalOutbox)
        {
            _supplyDbContext.TransactionalOutboxList.Add(transactionalOutbox);
        }
        #endregion
    }
}
