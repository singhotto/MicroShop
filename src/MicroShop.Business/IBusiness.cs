using MicroShop.Repository.Model;
using MicroShop.Shared.Dto;
using Microsoft.AspNetCore.Identity;

namespace MicroShop.Business
{
    public interface IBusiness
    {
        public Task<List<ProductDto>?> GetProducts(CancellationToken cancellationToken = default);

        public CartDto AddToCart(CartInsertDto item);

        public List<CartDto> GetCartItems(string userEmail);
        public Task<List<CartDto>?> GetCartItemsAuth(string accessToken, CancellationToken cancellationToken = default);
        public void emptyCart(string userEmail);
        public int IncreaseCartItems(string userEmail, int itemId);
        public int DecreaseCartItems(string userEmail, int itemId);
        public Task<UserDto> MakeSupplierAsync(string userEmail);
    }
}
