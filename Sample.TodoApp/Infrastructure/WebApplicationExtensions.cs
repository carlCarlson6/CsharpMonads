using FastEndpoints;
using FastEndpoints.Swagger;

namespace Sample.TodoApp.Infrastructure;

public static class WebApplicationExtensions
{
    public static WebApplication ConfigureWebApp(this WebApplication webApplication)
    {
        webApplication.UseAuthorization();
        webApplication.UseFastEndpoints();
        webApplication.UseOpenApi(); 
        webApplication.UseSwaggerUi3(c => c.ConfigureDefaults());

        return webApplication;
    }
}