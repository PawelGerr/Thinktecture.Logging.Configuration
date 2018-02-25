[![Build status](https://ci.appveyor.com/api/projects/status/ijstyxit8tn7baex?svg=true)](https://ci.appveyor.com/project/PawelGerr/thinktecture-logging-configuration)  
[![Thinktecture.Extensions.Logging.Configuration](https://img.shields.io/nuget/v/Thinktecture.Extensions.Logging.Configuration.svg?label=Thinktecture.Extensions.Logging.Configuration&maxAge=3600)](https://www.nuget.org/packages/Thinktecture.Extensions.Logging.Configuration/)  
[![Thinktecture.Extensions.Serilog.Configuration](https://img.shields.io/nuget/v/Thinktecture.Extensions.Serilog.Configuration.svg?label=Thinktecture.Extensions.Serilog.Configuration&maxAge=3600)](https://www.nuget.org/packages/Thinktecture.Extensions.Serilog.Configuration/)

Allows to change the log level at runtime.
It is usefull for pinpointing issues in production environments by changing to lower log level like `Debug` temporarily (for example via GUI or Web API) without restarting the application.

There are 2 projects/nuget packages:
* Use `Thinktecture.Extensions.Logging.Configuration` in case you are using `Microsoft.Extensions.Logging.ILogger`
* Use `Thinktecture.Extensions.Serilog.Configuration` with `Serilog.ILogger`

## Usage

The steps for setting up the configuration for `ILogger` from Microsoft and [Serilog](https://github.com/serilog/serilog) are almost identical.

### With *Microsoft.Extensions.Logging.ILogger*

[Example](https://github.com/PawelGerr/Thinktecture.Logging.Configuration/blob/master/example/Thinktecture.Extensions.Logging.Configuration.Example/Program.cs)

`Install-Package Thinktecture.Extensions.Logging.Configuration`

Create an instance of `ILoggingConfiguration`

```
// You can register this instance (i.e. ILoggingConfiguration) with DI
// and, for example, control the logging level via GUI or Web API
var loggingConfig = new LoggingConfiguration();
```

Add the logging configuration to your `IConfigurationBuilder` using extension method `AddLoggingConfiguration`.
This call must be placed after other configuration providers that change the log level, e.g. as the last one.

```
var config = new ConfigurationBuilder()
	// Adding JsonFile just to provide some defaults
	.AddJsonFile("appsettings.json", false, true)
	// The following line is the only one that's new.
	// The path to the logging config is "My:Logging" in this example
	.AddLoggingConfiguration(loggingConfig, "My", "Logging")
	.Build();

== appsettings.json =============================================
"My": {
	"Logging": {
		...
	}
}
```

Use the `IConfiguration` to configure the logger [the usual way](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging#log-filtering).

```
var serviceProvider = new ServiceCollection()
	.AddLogging(builder =>
	{
		// Nothing new, provide IConfiguration the usual way
		builder.AddConfiguration(config.GetSection("My:Logging"));
		builder.AddConsole();
	})
	.BuildServiceProvider();
```

Use `ILoggingConfiguration` to set and reset the log level.

```
ILoggingConfiguration loggingConfig = ...;

//Changing log level of category=Thinktecture and provider=all to Warning
loggingConfig.SetLevel(LogLevel.Warning, "Thinktecture");

// Changing log level of category=all and provider=Console to Error
loggingConfig.SetLevel(LogLevel.Error, null, "Console");

// Changing log level of category=Thinktecture and provider=Console to Critical
loggingConfig.SetLevel(LogLevel.Critical, "Thinktecture", "Console");

// Resetting all settings, returning to defaults
loggingConfig.ResetAll();
```

### With *Serilog*

[Example](https://github.com/PawelGerr/Thinktecture.Logging.Configuration/blob/master/example/Thinktecture.Extensions.Serilog.Configuration.Example/Program.cs)

`Install-Package Thinktecture.Extensions.Serilog.Configuration`

Create an instance of `ISerilogConfiguration`

```
// You can register this instance (i.e. ISerilogConfiguration) with DI
// and, for example, control the logging level via GUI or Web API
var loggingConfig = new SerilogConfiguration();
```

Add the logging configuration to your `IConfigurationBuilder` using extension method `AddLoggingConfiguration`.
This call must be placed after other configuration providers that change the log level, e.g. as the last one.

> **Limitation**: Serilog creates *a watcher* (i.e. a `LoggingLevelSwitch`) for configuration keys only that are present when building the logger.
> For example: if there is no configuration for `MinimumLevel:Override:Thinktecture` when CreateLogger() is called then you won't be able to change the log level for this category during runtime.

```
var config = new ConfigurationBuilder()
	// Adding JsonFile to provide defaults.
	.AddJsonFile("appsettings.json", false, true)
	// The following line is the only one that's new.
	// The path to the logging config is "My:Serilog" in this example
	.AddLoggingConfiguration(loggingConfig, "My", "Serilog")
	.Build();

== appsettings.json =============================================

"My": {
	"Serilog": {
		...
	}
}
```

Use the `IConfiguration` to configure Serilog according to [Serilog.Settings.Configuration](https://github.com/serilog/serilog-settings-configuration)

```
var loggerConfiguration = new LoggerConfiguration()
	.ReadFrom.ConfigurationSection(config.GetSection("My:Serilog"))
	.WriteTo.Console();
```

Use `ISerilogConfiguration` to set and reset log level.

```
ISerilogConfiguration serilogConfig = ...;

// Changing log level of category=all to Information
serilogConfig.SetLevel(LogEventLevel.Information);

// Changing log level of category=Thinktecture to Debug
serilogConfig.SetLevel(LogEventLevel.Debug, "Thinktecture");

Resetting all settings, returning to defaults
serilogConfig.ResetAll();
```
