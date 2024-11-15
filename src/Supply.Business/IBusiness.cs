using Supply.Shared.Dto;

namespace Supply.Business
{
    public interface IBusiness
    {
        #region Suppliers
        public List<SupplierDto> GetSuppliers();

        public SupplierDto? GetSupplier(string id);
        public Task<SupplierDto?> AddSupplier(string supplierEmail, string accessToken);
        public SupplierDto? UpdateSupplier(string id, SupplierDto supplier);
        public void DeleteSupplier(string id);

        #endregion Suppliers

        #region Orders
        public List<OrderDto> GetOrders();

        public OrderDto? GetOrder(int id);
        public List<OrderProductDto> GetOrderOfSupplier(string supplierId, string status = "", DateTime from = default, DateTime to = default);
        public OrderDto AddOrder(string supplierId, List<OrderProductInsertDto> productList);
        public OrderDto? UpdateOrder(int id, OrderDto order);
        public OrderDto? UpdateOrderStatus(int id, string status);
        public void DeleteOrder(int id);

        #endregion Orders

        #region Products

        public List<ProductDto> GetProducts();
        public ProductDto? GetProduct(string id);
        public List<ProductDto>? GetProductOfSupplier(string supplierId);
        public void AddProducts(List<ProductInsertDto> products);
        public void AddRandomProducts(string supplierId);

        #endregion Products
    }
}
