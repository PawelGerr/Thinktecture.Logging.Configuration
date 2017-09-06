using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Thinktecture.Extensions.Configuration;

namespace Thinktecture.Extensions.Serilog.Configuration.Example
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// You can register this instance (i.e. ISerilogConfiguration) with DI
			// and, for example, control the logging level via GUI or Web API
			var loggingConfig = new SerilogConfiguration();

			var config = new ConfigurationBuilder()
				// Adding JsonFile to provide defaults.
				// Limitation: Serilog creates "a watcher" for keys that are present when building the logger.
				// For example: if there is no configuration for "MinimumLevel:Override:Thinktecture" when CreateLogger() is called
				// then you won't be able to change the log level for this category.
				.AddJsonFile("appsettings.json", false, true)
				// The following line is the only one that's new.
				// The path to the logging config is "My:Serilog" in this example
				.AddLoggingConfiguration(loggingConfig, "My", "Serilog")
				.Build();

			var loggerConfiguration = new LoggerConfiguration()
				.ReadFrom.ConfigurationSection(config.GetSection("My:Serilog"))
				.WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {SourceContext}: {Message:lj}{NewLine}{Exception}");

			var logger = loggerConfiguration.CreateLogger().ForContext<Program>();

			while (true)
			{
				GenerateLogs(logger, loggingConfig);

				Print("Press ENTER to generate logs again.");
				Console.ReadLine();
			}
		}

		private static void GenerateLogs(ILogger logger, ISerilogConfiguration serilogConfig)
		{
			Print("Default settings");
			GenerateLogs(logger);

			Print($"Changing log level of category=all to {LogEventLevel.Error}");
			serilogConfig.SetLevel(LogEventLevel.Error);
			GenerateLogs(logger);

			Print($"Changing log level of category=Thinktecture to {LogEventLevel.Fatal}");
			serilogConfig.SetLevel(LogEventLevel.Fatal, "Thinktecture");
			GenerateLogs(logger);

			Print("Resetting all settings, returning to defaults");
			serilogConfig.ResetAll();
			GenerateLogs(logger);
		}

		private static void GenerateLogs(ILogger logger)
		{
			var logLevels = Enum.GetValues(typeof(LogEventLevel)).Cast<LogEventLevel>();

			foreach (var level in logLevels)
			{
				logger.Write(level, Enum.GetName(typeof(LogEventLevel), level));
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
