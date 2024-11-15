using Supply.Client.Http.Abstractions;
using Supply.Shared.Dto;
using System.Net.Http.Json;
using System.Text.Json;

namespace Supply.Client.Http;

public class SupplyClient : ISupplyClient
{

    private readonly HttpClient _httpClient;

    public SupplyClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SupplierDto?> PostApiData(Supplier postData, string endpoint, string accessToken = "")
    {
        try
        {
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Add("Cookie", accessToken);
            }
            string jsonData = System.Text.Json.JsonSerializer.Serialize(postData);
            var content = new StringContent(jsonData, System.Text.Encoding.UTF8, "application/json");
            Console.WriteLine("Contents: " + await content.ReadAsStringAsync());

            HttpResponseMessage response = await _httpClient.PostAsync(endpoint, content);

            // Check if the request was successful (0000000000000000status code 2xx)
            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Final Data:       " + responseContent);
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                // Read the response content and deserialize it to the specified type
                SupplierDto ? responseData = await response.Content.ReadFromJsonAsync<SupplierDto>(options);

                Console.WriteLine("Response DATA: " + responseData.Address + " " + responseData.LastName );
                return responseData;
            }
            else
            {
                // Handle the case where the request was not successful
                // You might want to throw an exception or log the error
                Console.WriteLine(response);
                return null;
            }
        }
        finally
        {
            // Clear the "Cookie" header to avoid affecting other requests
            _httpClient.DefaultRequestHeaders.Remove("Cookie");
        }
    }

    public async Task<SupplierDto?> AddSupplierHttp(string supplierEmail, string accessToken)
    {
        Supplier s = new Supplier();
        s.supplierEmail = supplierEmail;
        return await PostApiData(s, "https://microshop.webapp/MicroShop/MakeSupplier", accessToken);
    }

    public class Supplier
    {
        public string supplierEmail { get; set; } = string.Empty;
    }
}