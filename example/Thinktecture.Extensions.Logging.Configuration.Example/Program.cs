using System;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Thinktecture.Extensions.Configuration;

namespace Thinktecture.Extensions.Logging.Configuration.Example
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// You can register this instance (i.e. ILoggingConfiguration) with DI
			// and, for example, control the logging level via GUI or Web API
			var loggingConfig = new LoggingConfiguration();

			var config = new ConfigurationBuilder()
				// Adding JsonFile just to provide some defaults
				.AddJsonFile("appsettings.json", false, true)
				// The following line is the only one that's new.
				// The path to the logging config is "My:Logging" in this example
				.AddLoggingConfiguration(loggingConfig, "My", "Logging")
				.Build();

			var serviceProvider = new ServiceCollection()
				.AddLogging(builder =>
				{
					// Nothing new, provide IConfiguration the usual way
					// (https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging#log-filtering)
					builder.AddConfiguration(config.GetSection("My:Logging"));
					builder.AddConsole();
				})
				.BuildServiceProvider();

			// Generate some logs
			var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

			while (true)
			{
				GenerateLogs(logger, loggingConfig);

				Print("Press ENTER to generate logs again.");
				Console.ReadLine();
			}
		}

		private static void GenerateLogs(ILogger logger, ILoggingConfiguration loggingConfig)
		{
			Print("Default settings");
			GenerateLogs(logger);

			Print($"Changing log level of category=Thinktecture and provider=all to {LogLevel.Warning}");
			loggingConfig.SetLevel(LogLevel.Warning, "Thinktecture");
			GenerateLogs(logger);

			Print($"Changing log level of category=all and provider=Console to {LogLevel.Error}");
			loggingConfig.SetLevel(LogLevel.Error, null, "Console");
			GenerateLogs(logger);

			Print($"Changing log level of category=Thinktecture and provider=Console to {LogLevel.Critical}");
			loggingConfig.SetLevel(LogLevel.Critical, "Thinktecture", "Console");
			GenerateLogs(logger);

			Print("Resetting all settings, returning to defaults");
			loggingConfig.ResetLevel();
			GenerateLogs(logger);
		}

		private static void GenerateLogs(ILogger logger)
		{
			var logLevels = Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>();

			foreach (var level in logLevels.Where(l => l != LogLevel.None))
			{
				logger.Log(level, 0, level, null, (l, ex) => Enum.GetName(typeof(LogLevel), l));
			}

			Thread.Sleep(100); // wait for console to finish output
		}

		private static void Print(string message)
		{
			Console.WriteLine();
			Console.WriteLine(message);
			Console.WriteLine();
		}
	}
}
