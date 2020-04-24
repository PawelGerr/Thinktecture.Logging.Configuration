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

		/// <inheritdoc />
		public void SetLevel(LogLevel level, string? category = null, string? provider = null)
		{
			foreach (var p in _providers)
			{
				p.SetLevel(level, category, provider);
			}
		}

		/// <inheritdoc />
		public void ResetLevel(string? category = null, string? provider = null)
		{
			foreach (var p in _providers)
			{
				p.ResetLevel(category, provider);
			}
		}

		/// <inheritdoc />
		public void ResetAll()
		{
			foreach (var p in _providers)
			{
				p.ResetAll();
			}
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
