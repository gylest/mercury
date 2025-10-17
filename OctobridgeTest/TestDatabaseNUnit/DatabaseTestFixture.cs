namespace Tests;

public abstract class DatabaseTestFixture
{
    protected OctobridgeContext Context { get; private set; }

    [NUnit.Framework.SetUp]
    public void BaseSetup()
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = config.GetConnectionString("OctobridgeDatabaseConnection");

        var options = new DbContextOptionsBuilder<OctobridgeContext>()
            .UseSqlServer(connectionString)
            .Options;

        Context = new OctobridgeContext(options);
    }
}
