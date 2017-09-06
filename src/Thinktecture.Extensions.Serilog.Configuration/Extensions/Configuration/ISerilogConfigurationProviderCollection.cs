namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// A collection of <see cref="ISerilogConfigurationProvider"/>.
	/// </summary>
	public interface ISerilogConfigurationProviderCollection
	{
		/// <summary>
		/// Number of providers.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Adds a provider to collection.
		/// </summary>
		/// <param name="provider">Provider to add.</param>
		void Add(ISerilogConfigurationProvider provider);

		/// <summary>
		/// Removes a provider from collection.
		/// </summary>
		/// <param name="provider">Provider to remove.</param>
		void Remove(ISerilogConfigurationProvider provider);
	}
}
