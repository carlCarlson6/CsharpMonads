using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Sample.TodoApp.Infrastructure;
using Sample.TodoApp.Infrastructure.RavenDb;

namespace Sample.TodoApp.Tests.Api;

public class BaseApiTestWithRavenDb : BaseRavenTestDriver
{
    protected IWebHost GivenTestHost() =>
        WebHost
            .CreateDefaultBuilder()
            .UseStartup<Startup>()
            .UseEnvironment(HostEnvironmentExtensions.ApiTestsEnvironmentName)
            .UseTestServer()
            .ConfigureTestServices(services =>
            {
                services.AddFakeAuthenticationHandler();
                services
                    .AddSingleton(DocumentStore)
                    .AddMigrationAutoRunAndCreateIndexes(DocumentStore);
            })
            .UseDefaultServiceProvider((_, options) =>
            {
                
                // makes sure DI lifetimes and scopes don't have common issues
                options.ValidateScopes = true;
                options.ValidateOnBuild = true;
            })
            .Start();
}