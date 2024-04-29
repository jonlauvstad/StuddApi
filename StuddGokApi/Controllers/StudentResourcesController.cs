using Microsoft.AspNetCore.Mvc;
using StuddGokApi.Models;
using StudentResource.Models;
using StudentResource.Services.Interfaces;

namespace StuddGokApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentResourcesController : ControllerBase
    {
        private readonly IStudentResourceService _studentResourceService;
        private readonly ILogger<StudentResourcesController> _logger;

        public StudentResourcesController(IStudentResourceService studentResourceService, ILogger<StudentResourcesController> logger)
        {
            _studentResourceService = studentResourceService;
            _logger = logger;
        }

        [HttpGet("{courseId}")]
        public IActionResult GetResourcesForCourse(int courseId)
        {
            string? traceId = System.Diagnostics.Activity.Current?.Id;
            _logger.LogDebug("Class:{class}, Function:{function} Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}",
                    "StudentResourcesController", "GetResourcesForCourse", $"/StudentResources/{courseId}", "GET", "In", traceId);

            IEnumerable<StudentResourceModel> resources = _studentResourceService.GetResourcesForCourse(courseId);

            _logger.LogDebug("Class:{class},Function:{function}, Url:{url}, Method:{method}, InOut:{inOut},\n\t\tTraceId:{traceId}, StatusCode:{statusCode}, NoVenues:{noVenues}",
                "StudentResourcesController", "GetResourcesForCourse", "/StudentResources/{courseId}", "GET", "Out", traceId, 400, resources.Count() == 0);

            return Ok(resources);
        }
    }
}
