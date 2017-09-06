using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class SetLevel : TestBase
	{
		private readonly Mock<ILoggingConfigurationProvider> _providerMock;

		public SetLevel()
		{
			_providerMock = new Mock<ILoggingConfigurationProvider>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_not_raise_error_if_not_providers_in_internal_collection()
		{
			var config = CreateConfig();
			config.SetLevel(LogLevel.Information);
		}

		[Fact]
		public void Should_delegate_the_call_to_provider()
		{
			_providerMock.Setup(p => p.SetLevel(LogLevel.Information, "Thinktecture", "Console"));

			var config = CreateConfig(out var collection);
			collection.Add(_providerMock.Object);

			config.SetLevel(LogLevel.Information, "Thinktecture", "Console");
			_providerMock.Verify(p => p.SetLevel(LogLevel.Information, "Thinktecture", "Console"), Times.Once);
		}
	}
}
