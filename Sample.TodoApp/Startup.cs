using FastEndpoints;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Sample.TodoApp.Infrastructure;

namespace Sample.TodoApp;

public class Startup
{
    // TODO move to config   
    private const string TokenSigningKey = ")H@McQfTjWnZr4u7x!z%C*F-JaNdRgUkXp2s5v8y/B?D(G+KbPeShVmYq3t6w9z$";
    
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _environment;

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        if (!_environment.RunningTests())
        {
            services.AddRavenDb(_configuration);
            services.AddAuthenticationJWTBearer(TokenSigningKey); 
        }

        services
            .AddTodoItemsServices()
            .AddFastEndpoints()
            .AddSwaggerDoc();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) => app
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseFastEndpointsMiddleware()
        .UseEndpoints(builder => builder.MapFastEndpoints())
        .UseApimundo()
        .UseOpenApi()
        .UseSwaggerUi3(c => 
            c.ConfigureDefaults());
}