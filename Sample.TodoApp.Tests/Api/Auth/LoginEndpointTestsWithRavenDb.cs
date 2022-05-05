using System.Net.Http.Json;
using System.Threading.Tasks;
using Sample.TodoApp.Auth.Login;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Sample.TodoApp.Auth;
using Sample.TodoApp.Infrastructure;
using Sample.TodoApp.Tests.Helpers;

namespace Sample.TodoApp.Tests.Api.Auth;

public class LoginEndpointTestsWithRavenDb : BaseApiTestWithRavenDb
{
    private const string UserName = "carlKarlson";
    private const string InputPassword = "1npuTP4$$0Rd";

    [Fact]
    public async Task GivenUser_WhenPostLoginEndpoint_ThenReturnsJwtToken()
    {
        await new UserBuilder()
            .With(TodoApp.Auth.UserName.From(UserName))
            .With(Password.From(InputPassword))
            .BuildAsync(DocumentStore);
        
        var httpResponse = await GivenTestHost().GetTestClient().PostAsJsonAsync(
            ApiUris.Login, 
            new LoginRequest { UserName = UserName, InputPassword = InputPassword });

        httpResponse.IsSuccessStatusCode.Should().BeTrue();
        var response = await httpResponse.Content.ReadFromJsonAsync<SuccessfulLoginResponse>(); 
        response!
            .Token
            .Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task GivenUser_WithInvalidCredentials_WhenPostLoginEndpoint_Then400InvalidCredentials()
    {
        await new UserBuilder()
            .With(TodoApp.Auth.UserName.From(UserName))
            .With(Password.From(InputPassword))
            .BuildAsync(DocumentStore);
        
        var httpResponse = await GivenTestHost().GetTestClient().PostAsJsonAsync(
            ApiUris.Login, 
            new LoginRequest { UserName = UserName, InputPassword = "invalid_password" });

        httpResponse.IsSuccessStatusCode.Should().BeFalse();
    }
}