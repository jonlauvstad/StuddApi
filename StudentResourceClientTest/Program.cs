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


    /*
    static async Task Main(string[] args)
    {
        var baseUrl = "https://localhost:7042";
        var httpClient = new HttpClient();

        // token generert fra flask funksjon (user 1)
        var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJnb2tzdGFkZW1haWwiOiJqb2hhbm5lcy5hbmRlcnNlbkBnb2tzdGFkYWthZGVtaWV0Lm5vIiwiZmlyc3RuYW1lIjoiSm9oYW5uZXMiLCJsYXN0bmFtZSI6IkFuZGVyc2VuIiwiaWQiOiIxIiwicm9sZSI6ImFkbWluIiwiZW1haWwyIjoiIiwiZW1haWwzIjoiIiwibGluayI6Ii9Vc2VyLzEiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJhZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWVpZGVudGlmaWVyIjoiam9oYW5uZXMuYW5kZXJzZW5AZ29rc3RhZGFrYWRlbWlldC5ubyIsImV4cCI6MTcwODQ3OTYzMywiaXNzIjoiaHR0cHM6Ly9qb3lkaXBrYW5qaWxhbC5jb20vIiwiYXVkIjoiaHR0cHM6Ly9qb3lkaXBrYW5qaWxhbC5jb20vIn0.tM_6iGAnmEnCCNVIDJm2uLxvqJgWA-zkU4jIzWiv_Nw";
        // var token = rawToken.Trim();

        var userClient = new UserClient(httpClient, baseUrl, token);

        var user = await userClient.GetUserByIdAsync(1);

        if (user != null)
        {
            Console.WriteLine($"User Found: {user.FirstName} {user.LastName}");
        }
        else
        {
            Console.WriteLine("User not found.");
        }
    }
    */
}
