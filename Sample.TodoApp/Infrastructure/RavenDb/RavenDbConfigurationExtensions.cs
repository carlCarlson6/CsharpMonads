using System.Security.Cryptography.X509Certificates;
using Raven.Client.Documents;
using Raven.Client.Documents.Conventions;
using Raven.Client.Documents.Indexes;
using Raven.Client.Json.Serialization.NewtonsoftJson;

namespace Sample.TodoApp.Infrastructure.RavenDb;

public static class RavenDbConfigurationExtensions
{
    public static (RavenDatabaseSetting dbSettings, X509Certificate2? certificate) GetRavenDbSettings(this IConfiguration configuration, string sectionName = null)
    {
        var dbSettings = new RavenDatabaseSetting();
        configuration.Bind(sectionName ?? nameof(RavenDatabaseSetting), dbSettings);

        X509Certificate2? certificate;
        if (!string.IsNullOrWhiteSpace(dbSettings.Thumbprint))
        {
            certificate = LoadByThumbprint(dbSettings.Thumbprint);
        }
        else
        {
            certificate = !string.IsNullOrEmpty(dbSettings.CertPath)
                ? new X509Certificate2(dbSettings.CertPath, dbSettings.CertPass)
                : null;
        }

        return (dbSettings, certificate);
    }

    private static X509Certificate2? LoadByThumbprint(string thumbprint)
    {
        using var certStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        certStore.Open(OpenFlags.ReadOnly);

        // ReSharper disable once RedundantEnumerableCastCall
        var cert = certStore.Certificates.OfType<X509Certificate2>()
            .FirstOrDefault(x => x.Thumbprint == thumbprint);

        return cert;
        
    }
    
    public static IServiceCollection AddMigrationAutoRunAndCreateIndexes(this IServiceCollection services, IDocumentStore store)
    {
        IndexCreation.CreateIndexes(typeof(Startup).Assembly, store);
        return services;
    }

    public static DocumentConventions RavenConventions() => new();

}