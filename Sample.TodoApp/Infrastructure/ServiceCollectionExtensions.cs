using Raven.Client.Documents;
using Sample.TodoApp.Abstractions;
using Sample.TodoApp.Infrastructure.RavenDb;
using Sample.TodoApp.TodoItems;
using Sample.TodoApp.TodoItems.CreateItem;
using Sample.TodoApp.TodoItems.GetItem;

namespace Sample.TodoApp.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRavenDb(this IServiceCollection services, IConfiguration configuration)
    {
        var (dbSettings, cert) = configuration.GetRavenDbSettings();
        var store = new DocumentStore
        {
            Urls = dbSettings.Urls,
            Database = dbSettings.DatabaseName,
            Certificate = cert,
            Conventions = RavenDbConfigurationExtensions.RavenConventions()
        }.Initialize();
        
        services.AddSingleton(store);
        return services;
    }

    public static IServiceCollection AddTodoItemsServices(this IServiceCollection service) =>
        service
            .AddSingleton<IDomainService<GetItemRequest, TodoItem>, TodoItemFinder>()
            .AddSingleton<IDomainService<CreateItemRequest, TodoItemId>, UserTodoItemsCreator>();
}