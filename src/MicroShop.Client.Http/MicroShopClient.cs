using MicroShop.Client.Http.Abstractions;
using MicroShop.Shared.Dto;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace MicroShop.Client.Http;

public class MicroShopClient : IMicroShopClient
{

    readonly HttpClient _httpClient;

    public MicroShopClient(HttpClient httpClient) {
        _httpClient = httpClient;
    }
    public async Task<List<T>?> GetApiData<T>(string endpoint, string accessToken = "", CancellationToken cancellationToken = default)
    {
        if(accessToken != "") 
            _httpClient.DefaultRequestHeaders.Add("Cookie", accessToken);
        HttpResponseMessage response = await _httpClient.GetAsync(endpoint, cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("Called GetApiData");
            // Read the content as a string
            string content = await response.Content.ReadAsStringAsync();

            // Log the content
            Console.WriteLine(content);
            return await response.Content.ReadFromJsonAsync<List<T>>((JsonSerializerOptions?)null, cancellationToken);
        }
        else if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }
        else
        {
            throw new Exception($"The API call to '{endpoint}' returned StatusCode: {response.StatusCode}");
        }
    }

    public async Task<List<CartDto>?> GetCartItems(string accessToken, CancellationToken cancellationToken = default)
    {
        Console.WriteLine("Called GetCartItems");
        List<CartDto> cartItems = await GetApiData<CartDto>("https://microshop.webapp/MicroShop/GetCartItems", accessToken, cancellationToken);
        Console.Write(cartItems.ToString());
        return cartItems;
    }

    public async Task<List<ProductDto>?> GetProducts(CancellationToken cancellationToken = default)
    {
        return await GetApiData<ProductDto>("https://warehouse.api/Warehouse/GetAllProducts", "", cancellationToken);
    }

}