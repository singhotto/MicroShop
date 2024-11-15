using MicroShop.Shared.Dto;

namespace MicroShop.Client.Http.Abstractions;
public interface IMicroShopClient {

    Task<List<ProductDto>?> GetProducts(CancellationToken cancellationToken = default);
    Task<List<CartDto>?> GetCartItems(string accessToken, CancellationToken cancellationToken = default);

}
