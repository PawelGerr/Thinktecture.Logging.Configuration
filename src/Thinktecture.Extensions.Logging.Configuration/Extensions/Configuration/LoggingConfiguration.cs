using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Reconfigures <see cref="ILogger"/> and <see cref="ILogger{TCategoryName}"/> at runtime.
	/// </summary>
	public class LoggingConfiguration : ILoggingConfiguration, ILoggingConfigurationProviderCollection
	{
		private readonly List<ILoggingConfigurationProvider> _providers;

		/// <summary>
		/// Initializes new instance of <see cref="LoggingConfiguration"/>.
		/// </summary>
		public LoggingConfiguration()
		{
			_providers = new List<ILoggingConfigurationProvider>();
		}

		/// <summary>
		/// Sets <paramref name="level"/> for provided <paramref name="category"/> and <paramref name="provider"/> if provided.
		/// </summary>
		/// <param name="level">Log level.</param>
		/// <param name="category">Logging category.</param>
		/// <param name="provider">Logging provider.</param>
		public void SetLevel(LogLevel level, string category = null, string provider = null)
		{
			_providers.ForEach(p => p.SetLevel(level, category, provider));
		}

		/// <summary>
		/// Resets the log level for provided <paramref name="category"/> and <paramref name="provider"/>.
		/// </summary>
		/// <param name="category">Logging category.</param>
		/// <param name="provider">Logging provider.</param>
		public void ResetLevel(string category = null, string provider = null)
		{
			_providers.ForEach(p => p.ResetLevel(category, provider));
		}

		/// <summary>
		/// Removed all previously made settings.
		/// </summary>
		public void ResetAll()
		{
			_providers.ForEach(p => p.ResetAll());
		}

		/// <inheritdoc />
		int ILoggingConfigurationProviderCollection.Count => _providers.Count;

		/// <inheritdoc />
		void ILoggingConfigurationProviderCollection.Add(ILoggingConfigurationProvider provider)
		{
			if (provider == null)
				throw new ArgumentNullException(nameof(provider));

			_providers.Add(provider);
		}

		/// <inheritdoc />
		void ILoggingConfigurationProviderCollection.Remove(ILoggingConfigurationProvider provider)
		{
			if (provider == null)
				throw new ArgumentNullException(nameof(provider));

			_providers.Remove(provider);
		}
	}
}