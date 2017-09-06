using Moq;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class TestBase
	{
		protected readonly Mock<ILoggingConfigurationProvider> ProviderMock = new Mock<ILoggingConfigurationProvider>(MockBehavior.Strict);

		protected static LoggingConfiguration CreateConfig()
		{
			return CreateConfig(out var collection);
		}

		protected static LoggingConfiguration CreateConfig(out ILoggingConfigurationProviderCollection collection)
		{
			var config = new LoggingConfiguration();
			collection = config;
			return config;
		}
	}
}
