using System;
using System.Collections.Generic;
using Serilog;
using Serilog.Events;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Reconfigures <see cref="ILogger"/> at runtime.
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
		public void SetLevel(LogEventLevel level, string category = null)
		{
			_providers.ForEach(p => p.SetLevel(level, category));
		}

		/// <inheritdoc />
		public void ResetLevel(string category = null)
		{
			_providers.ForEach(p => p.ResetLevel(category));
		}

		/// <inheritdoc />
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
