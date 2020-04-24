using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Changes logging configuration at runtime.
	/// </summary>
	public class LoggingConfigurationProvider : ConfigurationProvider, ILoggingConfigurationProvider
	{
		private readonly IEnumerable<string> _parentPath;

		/// <summary>
		/// Initializes new instance of <see cref="LoggingConfigurationProvider"/>.
		/// </summary>
		/// <param name="parentPath">Path to logging section.</param>
		public LoggingConfigurationProvider(IEnumerable<string> parentPath)
		{
			_parentPath = parentPath ?? throw new ArgumentNullException(nameof(parentPath));

			if (_parentPath.Any(String.IsNullOrWhiteSpace))
				throw new ArgumentException("The segments of the parent path must be null nor empty.");
		}

		/// <inheritdoc />
		public void SetLevel(LogLevel level, string? category = null, string? provider = null)
		{
			var path = BuildLogLevelPath(category, provider);
			Data[path] = GetLevelName(level);
			OnReload();
		}

		/// <inheritdoc />
		public void ResetLevel(string? category = null, string? provider = null)
		{
			var path = BuildLogLevelPath(category, provider);
			Data.Remove(path);
			OnReload();
		}

		/// <inheritdoc />
		public void ResetAll()
		{
			Data.Clear();
			OnReload();
		}

		private static string GetLevelName(LogLevel level)
		{
			if (!Enum.GetValues(typeof(LogLevel)).Cast<LogLevel>().Contains(level))
				throw new ArgumentException($"Provided value is not a valid {nameof(LogLevel)}: {level}", nameof(level));

			return Enum.GetName(typeof(LogLevel), level);
		}

		private string BuildLogLevelPath(string? category, string? provider)
		{
			var segments = _parentPath.ToList();

			if (!String.IsNullOrWhiteSpace(provider))
				segments.Add(provider!.Trim());

			segments.Add("LogLevel");
			segments.Add(String.IsNullOrWhiteSpace(category) ? "Default" : category!.Trim());
			return ConfigurationPath.Combine(segments);
		}
	}
}
