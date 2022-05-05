using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.TestHost;
using Sample.TodoApp.Health;
using Sample.TodoApp.Infrastructure;
using Xunit;

namespace Sample.TodoApp.Tests.Api.Health;

public class HealthEndpointTestsWithRavenDb : BaseApiTestWithRavenDb
{
    [Fact]
    public async Task GivenApi_WhenGetHealthEndpoint_ThenReturnsHelloWorldResponse()
    {
        var response = await GivenTestHost().GetTestClient().GetFromJsonAsync<Response>(ApiUris.Health);
        response.Should().BeEquivalentTo(new Response());
    }
}