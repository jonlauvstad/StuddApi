using StuddGokApi.DTOs;
using StuddGokApi.Mappers;
using StuddGokApi.Models;
using StuddGokApi.Repositories;
using StuddGokApi.Repositories.Interfaces;
using StuddGokApi.Services.Interfaces;

namespace StuddGokApi.Services;

public class CourseImpService : ICourseImpService
{
    private readonly ICourseImpRepository _cimpRepo;
    private readonly IMapper<CourseImplementation, CourseImplementationDTO> _cimpMapper;
    private readonly IMapper<User, UserDTO> _userMapper;

    public CourseImpService(ICourseImpRepository cimpRepo, IMapper<CourseImplementation, CourseImplementationDTO> cimpMapper,
        IMapper<User, UserDTO> userMapper)
    {
        _cimpRepo = cimpRepo;
        _cimpMapper = cimpMapper;
        _userMapper = userMapper;
    }

    public async Task<IEnumerable<CourseImplementationDTO>> GetCourseImpsAsync(DateTime? startDate, DateTime? endDate, 
        (string role, int userId)? user = null)
    {
        IEnumerable<CourseImplementation> cimps = await _cimpRepo.GetCourseImpsAsync(startDate, endDate, user:user);
        return from cimp in cimps select _cimpMapper.MapToDTO(cimp);
    }

    public async Task<CourseImplementationDTO?> GetCourseImpByIdAsync(int id)
    {
        CourseImplementation? ci = await _cimpRepo.GetCourseImpByIdAsync(id);
        if (ci == null) { return null; }
        return _cimpMapper.MapToDTO(ci);
    }

    public async Task<IEnumerable<int>> GetQualifiedStudentIdsAsync(int courseImpId)
    {
        return await _cimpRepo.GetQualifiedStudentIdsAsync(courseImpId);
    }

    public async Task<IEnumerable<UserDTO>> GetQualifiedStudentObjectsAsync(int courseImpId)
    {
        IEnumerable<User> qStudents = await _cimpRepo.GetQualifiedStudentObjectsAsync(courseImpId);
        return qStudents.Select(x => _userMapper.MapToDTO(x));
    }
}
