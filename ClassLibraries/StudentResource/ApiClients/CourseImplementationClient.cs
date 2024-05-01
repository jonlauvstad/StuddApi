using Newtonsoft.Json;
using StudentResource.ApiClients.Interfaces;
using StudentResource.Models.POCO;
using System.Net.Http;
using System.Threading.Tasks;


public class CourseImplementationClient : ICourseImplementationClient
{
    private readonly HttpClient _httpClient;

    public CourseImplementationClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CourseImplementation> GetCourseImplementationByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"https://localhost:7042/api/v1/CourseImplementation/{id}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var courseImplementation = JsonConvert.DeserializeObject<CourseImplementation>(content); //  CourseImplementation POCO
            return courseImplementation;
        }

        return null; 
    }
}
