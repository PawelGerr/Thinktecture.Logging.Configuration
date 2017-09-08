using System;
using System.Collections.Generic;
using Serilog;
using Serilog.Events;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Reconfigures <see cref="ILogger"/> at runtime.
	/// </summary>
	public class SerilogConfiguration : ISerilogConfiguration, ISerilogConfigurationProviderCollection
	{
		private readonly List<ISerilogConfigurationProvider> _providers;

		/// <summary>
		/// Initializes new instance of <see cref="SerilogConfiguration"/>.
		/// </summary>
		public SerilogConfiguration()
		{
			_providers = new List<ISerilogConfigurationProvider>();
		}

		/// <inheritdoc />
		public void SetLevel(LogEventLevel level, string category = null)
		{
			foreach (var p in _providers)
			{
				p.SetLevel(level, category);
			}
		}

		/// <inheritdoc />
		public void ResetLevel(string category = null)
		{
			foreach (var p in _providers)
			{
				p.ResetLevel(category);
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
		int ISerilogConfigurationProviderCollection.Count => _providers.Count;

		/// <inheritdoc />
		void ISerilogConfigurationProviderCollection.Add(ISerilogConfigurationProvider provider)
		{
			if (provider == null)
				throw new ArgumentNullException(nameof(provider));

			_providers.Add(provider);
		}

		/// <inheritdoc />
		void ISerilogConfigurationProviderCollection.Remove(ISerilogConfigurationProvider provider)
		{
			if (provider == null)
				throw new ArgumentNullException(nameof(provider));

			_providers.Remove(provider);
		}
	}
}
