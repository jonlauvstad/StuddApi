using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDockerDb;

public class BaseIntegrationTests : IClassFixture<StuddGokApiWebAppFactory>, IDisposable
{
    public HttpClient Client { get; init; }
    protected readonly IServiceScope _scope;
    //public IUserService? UserService { get; init; }
    public BaseIntegrationTests(StuddGokApiWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Client = factory.CreateClient();
        //UserService = _scope.ServiceProvider.GetService<IUserService>();
    }

    protected T? GetService<T>() where T : class
    {
        return _scope.ServiceProvider.GetService<T>();
    }

    public void Dispose()
    {
        Client?.Dispose();
    }
}
