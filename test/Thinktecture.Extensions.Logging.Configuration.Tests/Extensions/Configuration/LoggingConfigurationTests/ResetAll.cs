using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class ResetAll : TestBase
	{
		private readonly Mock<ILoggingConfigurationProvider> _providerMock;

		public ResetAll()
		{
			_providerMock = new Mock<ILoggingConfigurationProvider>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_not_raise_error_if_not_providers_in_internal_collection()
		{
			var config = CreateConfig();
			config.ResetAll();
		}

		[Fact]
		public void Should_delegate_the_call_to_provider()
		{
			_providerMock.Setup(p => p.ResetAll());

			var config = CreateConfig(out var collection);
			collection.Add(_providerMock.Object);

			config.ResetAll();
			_providerMock.Verify(p => p.ResetAll(), Times.Once);
		}
	}
}
