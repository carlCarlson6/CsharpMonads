using System;
using System.Threading.Tasks;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;
using Sample.TodoApp.Infrastructure.RavenDb;

namespace Sample.TodoApp.Tests.Api;

public abstract class BaseRavenTestDriver : RavenTestDriver
{
    protected readonly IDocumentStore DocumentStore;

    protected BaseRavenTestDriver()
    {
        DocumentStore = GetDocumentStore(null, Guid.NewGuid().ToString());
    }

    protected override void PreInitialize(IDocumentStore documentStore)
    {
        var documentConventions = RavenDbConfigurationExtensions.RavenConventions();
        documentStore.Conventions.Serialization = documentConventions.Serialization;
        base.PreInitialize(documentStore);
    }

    protected void CreateIndexes()
    {
        IndexCreation.CreateIndexes(typeof(Startup).Assembly, DocumentStore);
    }

    protected async Task CreateIndexesAndWaitForNonStaleResults(params AbstractIndexCreationTask[] indexes)
    {
        await DocumentStore.ExecuteIndexesAsync(indexes);
        WaitForIndexing(DocumentStore);
    }
}