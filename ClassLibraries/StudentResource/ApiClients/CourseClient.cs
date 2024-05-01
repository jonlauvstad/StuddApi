using Newtonsoft.Json;
using StudentResource.ApiClients.Interfaces;
using StudentResource.Models.POCO;
using System.Net.Http;
using System.Threading.Tasks;

public class CourseClient : ICourseClient
{
    private readonly HttpClient _httpClient;

    public CourseClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Course> GetCourseByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7042/api/v1/Course/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var course = JsonConvert.DeserializeObject<Course>(content); // Course POCO
            return course;
        }

        return null; 
    }
}
