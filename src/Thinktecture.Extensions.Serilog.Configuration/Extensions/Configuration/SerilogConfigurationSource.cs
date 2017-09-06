using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Logging configuration source for changing logging configuration at runtime.
	/// </summary>
	public class SerilogConfigurationSource : IConfigurationSource
	{
		private readonly ISerilogConfigurationProviderCollection _providerCollection;
		private readonly IEnumerable<string> _parentPath;

		/// <summary>
		/// Initializes new instance of <see cref="SerilogConfigurationSource"/>.
		/// </summary>
		/// <param name="providerCollection">Logging configuration provider collection that newly created providers are going to be added in.</param>
		/// <param name="parentPath">Path to logging section.</param>
		public SerilogConfigurationSource(ISerilogConfigurationProviderCollection providerCollection, params string[] parentPath)
		{
			_providerCollection = providerCollection ?? throw new ArgumentNullException(nameof(providerCollection));
			_parentPath = parentPath ?? throw new ArgumentNullException(nameof(parentPath));

			if (_parentPath.Any(String.IsNullOrWhiteSpace))
				throw new ArgumentException("The segments of the parent path must be null nor empty.");
		}

		/// <inheritdoc />
		public IConfigurationProvider Build(IConfigurationBuilder builder)
		{
			var provider = new SerilogConfigurationProvider(_parentPath);
			_providerCollection.Add(provider);

			return provider;
		}
	}
}
