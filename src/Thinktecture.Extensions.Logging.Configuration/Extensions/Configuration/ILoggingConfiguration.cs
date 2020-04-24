using Microsoft.Extensions.Logging;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Reconfigures <see cref="ILogger"/> and <see cref="ILogger{TCategoryName}"/> at runtime.
	/// </summary>
	public interface ILoggingConfiguration
	{
		/// <summary>
		/// Sets <paramref name="level"/> for provided <paramref name="category"/> and <paramref name="provider"/> if provided.
		/// </summary>
		/// <param name="level">Log level.</param>
		/// <param name="category">Logging category.</param>
		/// <param name="provider">Logging provider.</param>
		void SetLevel(LogLevel level, string? category = null, string? provider = null);

		/// <summary>
		/// Resets the log level for provided <paramref name="category"/> and <paramref name="provider"/>.
		/// </summary>
		/// <param name="category">Logging category.</param>
		/// <param name="provider">Logging provider.</param>
		void ResetLevel(string? category = null, string? provider = null);
	}
}
