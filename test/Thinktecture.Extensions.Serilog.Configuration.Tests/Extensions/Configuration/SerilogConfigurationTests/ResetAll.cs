using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationTests
{
	public class ResetAll : TestBase
	{
		[Fact]
		public void Should_not_raise_error_if_not_providers_in_internal_collection()
		{
			var config = CreateConfig();
			config.ResetAll();
		}

		[Fact]
		public void Should_delegate_the_call_to_provider()
		{
			ProviderMock.Setup(p => p.ResetAll());

			var config = CreateConfig(out var collection);
			collection.Add(ProviderMock.Object);

			config.ResetAll();
			ProviderMock.Verify(p => p.ResetAll(), Times.Once);
		}
	}
}
