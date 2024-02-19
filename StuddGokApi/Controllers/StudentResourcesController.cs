using Microsoft.AspNetCore.Mvc;
using StudentResource.Services.Interfaces;

namespace StuddGokApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class StudentResourcesController : ControllerBase
    {
        private readonly IStudentResourceService _studentResourceService;

        public StudentResourcesController(IStudentResourceService studentResourceService)
        {
            _studentResourceService = studentResourceService;
        }

        [HttpGet("{courseId}")]
        public IActionResult GetResourcesForCourse(int courseId)
        {
            var resources = _studentResourceService.GetResourcesForCourse(courseId);
            return Ok(resources);
        }
    }
}
