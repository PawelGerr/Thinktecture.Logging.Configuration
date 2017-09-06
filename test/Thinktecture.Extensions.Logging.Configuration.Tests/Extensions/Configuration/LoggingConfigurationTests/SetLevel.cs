using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class SetLevel : TestBase
	{
		[Fact]
		public void Should_not_raise_error_if_not_providers_in_internal_collection()
		{
			var config = CreateConfig();
			config.SetLevel(LogLevel.Information);
		}

		[Fact]
		public void Should_delegate_the_call_to_provider()
		{
			ProviderMock.Setup(p => p.SetLevel(LogLevel.Information, "Thinktecture", "Console"));

			var config = CreateConfig(out var collection);
			collection.Add(ProviderMock.Object);

			config.SetLevel(LogLevel.Information, "Thinktecture", "Console");
			ProviderMock.Verify(p => p.SetLevel(LogLevel.Information, "Thinktecture", "Console"), Times.Once);
		}
	}
}
