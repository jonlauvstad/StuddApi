using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class CourseImpService : ICourseImpService
{
    private readonly ICourseImpRepository _cimpRepo;
    private readonly IMapper<CourseImplementation, CourseImplementationDTO> _cimpMapper;

    public CourseImpService(ICourseImpRepository cimpRepo, IMapper<CourseImplementation, CourseImplementationDTO> cimpMapper)
    {
        _cimpRepo = cimpRepo;
        _cimpMapper = cimpMapper;
    }

    public async Task<IEnumerable<CourseImplementationDTO>> GetCourseImpsAsync()
    {
        IEnumerable<CourseImplementation> cimps = await _cimpRepo.GetCourseImpsAsync();
        return from cimp in cimps select _cimpMapper.MapToDTO(cimp);
    }
}
