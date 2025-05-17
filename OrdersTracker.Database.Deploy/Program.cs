using System.Reflection;
using DbUp;
using DbUp.Engine;
using DbUp.Support;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace OrdersTracker.Database.Deploy;

public class Program
{
  public static int Main()
  {
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(LoadConfiguration())
        .CreateLogger();

    var configuration = LoadConfiguration();

    try
    {
      var connectionString = configuration.GetConnectionString("DatabaseConnection");
      var environment = configuration["Environment"];
      var result = DeployDatabase(connectionString!, environment!);

      Log.Information("Database {Result}", result ? "deployed" : "failed");

      return result ? 0 : 1;
    }
    catch (Exception ex)
    {
      Log.Fatal(ex, "An unhandled exception occurred during deployment.");

      return 1;
    }
    finally
    {
      Log.CloseAndFlush();
    }
  }

  private static IConfigurationRoot LoadConfiguration()
    => new ConfigurationBuilder()
        .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: true)
        .Build();

  private static bool DeployDatabase(string connectionString, string environment)
  {
    if (environment == "LocalRun")
    {
      ResetDatabase(connectionString);
    }

    var assembly = Assembly.GetExecutingAssembly();

    var upgrader = DeployChanges.To
        .SqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(assembly, script => script.Contains(".Scripts."),
            new SqlScriptOptions { RunGroupOrder = 1, ScriptType = ScriptType.RunOnce })
        .WithScriptsEmbeddedInAssembly(assembly, script => script.Contains(".StoredProcedures."),
            new SqlScriptOptions { RunGroupOrder = 2, ScriptType = ScriptType.RunOnce })
        .LogTo(new DbUp.Engine.Output.ConsoleUpgradeLog())
        .Build();

    return upgrader.PerformUpgrade().Successful;
  }

  private static void ResetDatabase(string connectionString)
  {
    Log.Information("Resetting the database...");
    DropDatabase.For.SqlDatabase(connectionString);
    EnsureDatabase.For.SqlDatabase(connectionString);
    Log.Information("Database reset completed.");
  }
}