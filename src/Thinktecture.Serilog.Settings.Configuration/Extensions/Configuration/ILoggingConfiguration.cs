using Serilog;
using Serilog.Events;

namespace Thinktecture.Extensions.Configuration
{
	/// <summary>
	/// Reconfigures <see cref="ILogger"/> at runtime.
	/// </summary>
	public interface ILoggingConfiguration
	{
		/// <summary>
		/// Sets <paramref name="level"/> for provided <paramref name="category"/> if provided.
		/// </summary>
		/// <param name="level">Log level.</param>
		/// <param name="category">Logging category.</param>
		void SetLevel(LogEventLevel level, string category = null);

		/// <summary>
		/// Resets the log level for provided <paramref name="category"/>.
		/// </summary>
		/// <param name="category">Logging category.</param>
		void ResetLevel(string category = null);

		/// <summary>
		/// Removed all previously made settings.
		/// </summary>
		void ResetAll();
	}
}
