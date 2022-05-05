namespace Sample.TodoApp.Infrastructure.RavenDb;

public class RavenDatabaseSetting
{
    public string[] Urls { get; set; }
    public string DatabaseName { get; set; }
    public string CertPath { get; set; }
    public string CertPass { get; set; }

    public string Thumbprint { get; set; }
}

