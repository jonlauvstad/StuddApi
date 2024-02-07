using StuddGokApi.DTOs;

namespace StuddGokApi.Services.Interfaces;

public interface ILoginService
{
    Task<string?> LoginAsync(LoginDTO loginDTO);

}
