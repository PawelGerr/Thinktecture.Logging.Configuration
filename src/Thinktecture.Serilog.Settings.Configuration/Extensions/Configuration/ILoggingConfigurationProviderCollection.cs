namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// A collection of <see cref="ILoggingConfigurationProvider"/>.
	/// </summary>
	public interface ILoggingConfigurationProviderCollection
	{
		/// <summary>
		/// Number of providers.
		/// </summary>
		int Count { get; }

		/// <summary>
		/// Adds a provider to collection.
		/// </summary>
		/// <param name="provider">Provider to add.</param>
		void Add(ILoggingConfigurationProvider provider);

		/// <summary>
		/// Removes a provider from collection.
		/// </summary>
		/// <param name="provider">Provider to remove.</param>
		void Remove(ILoggingConfigurationProvider provider);
	}
}
