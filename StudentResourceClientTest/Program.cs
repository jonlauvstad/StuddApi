using System;
using System.Net.Http;
using System.Threading.Tasks;
using StudentResource.ApiClients; 

class Program
{
    static async Task Main(string[] args)
    {
        using var httpClient = new HttpClient();
        var response = await httpClient.GetAsync("https://localhost:7042/api/v1/User/1");
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine($"Status Code: {response.StatusCode}");
        Console.WriteLine($"Response: {content}");
    }


   
}
