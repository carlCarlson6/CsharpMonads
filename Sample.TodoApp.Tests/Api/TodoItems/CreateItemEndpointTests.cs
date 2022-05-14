using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Sample.TodoApp.Infrastructure;
using Sample.TodoApp.Tests.Helpers;
using Sample.TodoApp.TodoItems;
using Sample.TodoApp.TodoItems.CreateItem;
using Sample.TodoApp.TodoItems.Infrastructure;
using Xunit;

namespace Sample.TodoApp.Tests.Api.TodoItems;

public class CreateItemEndpointTests : BaseApiTestWithRavenDb
{
    [Fact]
    public async Task GivenUser_WithYetNoTodoItem_WhenPostItem_ThenCreateResultIsReturned_AndItemIsStored()
    {
        var request = new CreateItemRequest { Title = "this is the title", Description = "this is the description" }; 
        var httpResponse = await GivenTestHost()
            .GetTestClient(TestUsers.TestUserCarl)
            .PostAsJsonAsync(ApiUris.TodoItem, request);
        
        httpResponse.IsSuccessStatusCode.Should().BeTrue();

        (await httpResponse.Content.ReadFromJsonAsync<CreateItemResponse>())!
            .Should().BeEquivalentTo(new CreateItemResponse());

        using var session = DocumentStore.OpenAsyncSession();
        var userItems = await session.LoadUserTodoItemsAsync(UserTodoItemsId.From(TestUsers.TestUserCarl.Id));
        userItems.Items.Should().HaveCount(1);

        var item = userItems.Items.FirstOrDefault().Value;
        item.Title.Should().Be(request.Title);
        item.Description.Should().Be(request.Description);
        item.Status.Should().Be(Status.Pending);
    }
}