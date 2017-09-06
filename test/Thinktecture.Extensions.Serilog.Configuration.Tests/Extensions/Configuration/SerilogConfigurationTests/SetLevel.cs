using Moq;
using Serilog.Events;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationTests
{
	public class SetLevel : TestBase
	{
		[Fact]
		public void Should_not_raise_error_if_not_providers_in_internal_collection()
		{
			var config = CreateConfig();
			config.SetLevel(LogEventLevel.Information);
		}

		[Fact]
		public void Should_delegate_the_call_to_provider()
		{
			ProviderMock.Setup(p => p.SetLevel(LogEventLevel.Information, "Thinktecture"));

			var config = CreateConfig(out var collection);
			collection.Add(ProviderMock.Object);

			config.SetLevel(LogEventLevel.Information, "Thinktecture");
			ProviderMock.Verify(p => p.SetLevel(LogEventLevel.Information, "Thinktecture"), Times.Once);
		}
	}
}
