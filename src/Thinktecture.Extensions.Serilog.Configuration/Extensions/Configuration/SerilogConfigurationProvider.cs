using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Serilog.Events;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Changes logging configuration at runtime.
	/// </summary>
	public class SerilogConfigurationProvider : ConfigurationProvider, ISerilogConfigurationProvider
	{
		private readonly IEnumerable<string> _parentPath;

		/// <summary>
		/// Initializes new instance of <see cref="SerilogConfigurationProvider"/>.
		/// </summary>
		/// <param name="parentPath">Path to logging section.</param>
		public SerilogConfigurationProvider(IEnumerable<string> parentPath)
		{
			_parentPath = parentPath ?? throw new ArgumentNullException(nameof(parentPath));

			if (_parentPath.Any(String.IsNullOrWhiteSpace))
				throw new ArgumentException("The segments of the parent path must be null nor empty.");
		}

		/// <inheritdoc />
		public void SetLevel(LogEventLevel level, string? category = null)
		{
			var path = BuildLogLevelPath(category, out var additionalPath);
			Data[path] = GetLevelName(level);

			if (!String.IsNullOrWhiteSpace(additionalPath))
				Data[additionalPath] = GetLevelName(level);

			OnReload();
		}

		/// <inheritdoc />
		public void ResetLevel(string? category = null)
		{
			var path = BuildLogLevelPath(category, out var additionalPath);
			Data.Remove(path);

			if (!String.IsNullOrWhiteSpace(additionalPath))
				Data.Remove(additionalPath);

			OnReload();
		}

		/// <inheritdoc />
		public void ResetAll()
		{
			Data.Clear();
			OnReload();
		}

		private static string GetLevelName(LogEventLevel level)
		{
			if (!Enum.GetValues(typeof(LogEventLevel)).Cast<LogEventLevel>().Contains(level))
				throw new ArgumentException($"Provided value is not a valid {nameof(LogEventLevel)}: {level}", nameof(level));

			return Enum.GetName(typeof(LogEventLevel), level);
		}

		private string BuildLogLevelPath(string? category, out string? additionalPath)
		{
			var segments = _parentPath.ToList();
			segments.Add("MinimumLevel");

			if (!String.IsNullOrWhiteSpace(category))
			{
				segments.Add("Override");
				segments.Add(category!.Trim());
				additionalPath = null;
			}
			else
			{
				additionalPath = ConfigurationPath.Combine(segments.Concat(new[] { "Default" }));
			}

			return ConfigurationPath.Combine(segments);
		}
	}
}
