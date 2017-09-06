using System;
using Microsoft.Extensions.Configuration;
using Thinktecture.Extensions.Configuration;

// ReSharper disable once CheckNamespace
namespace Thinktecture
{
	/// <summary>
	/// Extension methods for <see cref="IConfigurationBuilder"/>.
	/// </summary>
	public static class ConfigurationBuildExtensions
	{
		/// <summary>
		/// Adds logging configuration provider <paramref name="builder"/>.
		/// </summary>
		/// <param name="builder">Configuration builder the logging configuration to add to.</param>
		/// <param name="configuration">Logging configuration to add to the <paramref name="builder"/>.</param>
		/// <param name="parentPath">Path the the logging section.</param>
		/// <returns>An instance of <see cref="IConfigurationBuilder"/>.</returns>
		public static IConfigurationBuilder AddLoggingConfiguration(this IConfigurationBuilder builder, ILoggingConfigurationProviderCollection configuration, params string[] parentPath)
		{
			if (builder == null)
				throw new ArgumentNullException(nameof(builder));
			if (configuration == null)
				throw new ArgumentNullException(nameof(configuration));

			return builder.Add(new LoggingConfigurationSource(configuration, parentPath));
		}
	}
}
