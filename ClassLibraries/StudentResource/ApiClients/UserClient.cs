using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StudentResource.ApiClients.Interfaces;
using StudentResource.Models.POCO;
using System.Net.Http;
using System.Net.Http.Headers;

public class UserClient : IUserClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl;

    public UserClient(HttpClient httpClient, string baseUrl, string token)
    {
        _httpClient = httpClient;
        _baseUrl = baseUrl.TrimEnd('/') + "/api/v1/User/";
        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        try
        {
            var fullUrl = $"https://localhost:7042/api/v1/User/{id}";

            var response = await _httpClient.GetAsync(fullUrl); 
            Console.WriteLine($"Status Code: {response.StatusCode}");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response: {content}");

            if (response.IsSuccessStatusCode)
            {
                var user = JsonConvert.DeserializeObject<User>(content);
                return user;
            }
            else
            {
                
                Console.WriteLine($"Error: {content}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception: {ex.Message}");
            return null;
        }
    }

}
