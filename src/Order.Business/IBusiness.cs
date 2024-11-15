using Order.Shared.Dto;

namespace Order.Business
{
    public interface IBusiness
    {
        #region Orders
        public List<OrderDto> GetOrders();
        public OrderDto? GetOrder(int id);
        public List<OrderInfoDto> GetOrderOfUser(string userId, string status = "", DateTime from = default, DateTime to = default);
        public OrderDto AddOrder(OrderInsertDto order);
        public OrderDto? UpdateOrder(int id, OrderDto order);
        public OrderDto? UpdateOrderStatus(int id, string status);
        public void DeleteOrder(int id);

        #endregion Orders

        #region Products

        public List<ProductDto> GetProducts();
        public ProductDto? GetProduct(string id);
        public void AddProducts(List<ProductInsertDto> products);

        #endregion Products
    }
}
