using DbUp;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Respawn;

namespace OrdersTracker.IntegrationTests.Fixtures;

public class DatabaseFixture : IDisposable
{
  public string _connectionString { get; }
  private readonly Respawner _respawner;
  private readonly SqlConnection _connection;

  public DatabaseFixture()
  {
    var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.Test.json")
    .AddEnvironmentVariables()
    .Build();

    _connectionString = configuration["Database:ConnectionString"]!;

    EnsureDatabase.For.SqlDatabase(_connectionString);

    var upgrader = DeployChanges.To.SqlDatabase(_connectionString)
        .WithScript("TestDataScript", File.ReadAllText("Scripts/instnwnd_test.sql"))
        .LogTo(new DbUp.Engine.Output.ConsoleUpgradeLog())
        .Build();

    var result = upgrader.PerformUpgrade();
    if (!result.Successful)
    {
      throw new Exception("Database migration failed", result.Error);
    }

    _connection = new SqlConnection(_connectionString);
    _connection.Open();

    _respawner = Respawner
        .CreateAsync(_connection, new RespawnerOptions { DbAdapter = DbAdapter.SqlServer })
        .GetAwaiter()
        .GetResult();
  }

  public void Dispose()
  {
    DropDatabase.For.SqlDatabase(_connectionString);
    _connection.Dispose();
  }
}

[CollectionDefinition("IntegrationTestsCollection")]
public class DatabaseCollection : ICollectionFixture<DatabaseFixture>
{
  /*
  This class is used to apply [CollectionDefinition] and the ICollectionFixture<> interface.
  Defines a collection of tests that share the same context(the database fixture).
  This ensures that tests are not run in parallel, avoiding conflicts.
  */
}
