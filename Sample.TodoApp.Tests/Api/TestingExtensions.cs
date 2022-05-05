using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.TodoApp.Auth;
using Sample.TodoApp.Tests.Helpers;

namespace Sample.TodoApp.Tests.Api;

public static class TestingExtensions
{
    public static HttpClient GetTestClient(this IWebHost webHost, User user)
    {
        var client = webHost.GetTestClient();
        if (user is { })
        {
            client.DefaultRequestHeaders.Add("user", user.Id);
        }

        return client;
    }
    
    public static AuthenticationBuilder AddFakeAuthenticationHandler(this IServiceCollection services) =>
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(JwtBearerDefaults.AuthenticationScheme, options => { });
}

public class TestAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock)
        : base(options, logger, encoder, clock) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Context.Request.Headers.TryGetValue("user", out var userIdString);
        var user = TestUsers.All.FirstOrDefault(u => u.Id == userIdString);

        return user switch
        {
            null => Task.FromResult(AuthenticateResult.NoResult()),
            {} => OnFoundUser(user)
        };
    }

    private Task<AuthenticateResult> OnFoundUser(User user)
    {
        var claims = new[]
        {
            new Claim("UserName", user.Name.ToString()),
            new Claim("UserId", user.Id)
        };
        var identity = new ClaimsIdentity(claims, "Bearer");
        
        var principal = new ClaimsPrincipal(identity);
        
        var ticket = new AuthenticationTicket(principal, "Bearer");
        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}

