using Moq;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationTests
{
	public class TestBase
	{
		protected readonly Mock<ISerilogConfigurationProvider> ProviderMock = new Mock<ISerilogConfigurationProvider>(MockBehavior.Strict);

		protected static SerilogConfiguration CreateConfig()
		{
			return CreateConfig(out var collection);
		}

		protected static SerilogConfiguration CreateConfig(out ISerilogConfigurationProviderCollection collection)
		{
			var config = new SerilogConfiguration();
			collection = config;
			return config;
		}
	}
}
