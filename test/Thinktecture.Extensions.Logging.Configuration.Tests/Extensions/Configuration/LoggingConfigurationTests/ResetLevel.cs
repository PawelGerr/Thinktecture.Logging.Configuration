using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class ResetLevel : TestBase
	{
		[Fact]
		public void Should_not_raise_error_if_not_providers_in_internal_collection()
		{
			var config = CreateConfig();
			config.ResetLevel();
		}

		[Fact]
		public void Should_delegate_the_call_to_provider()
		{
			ProviderMock.Setup(p => p.ResetLevel("Thinktecture", "Console"));

			var config = CreateConfig(out var collection);
			collection.Add(ProviderMock.Object);

			config.ResetLevel("Thinktecture", "Console");
			ProviderMock.Verify(p => p.ResetLevel("Thinktecture", "Console"), Times.Once);
		}
	}
}
