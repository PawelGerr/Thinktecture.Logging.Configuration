using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationProviderTests
{
	public class ResetAll : TestBase
	{
		[Fact]
		public void Should_not_raise_errors_when_internal_collection_is_empty()
		{
			var provider = CreateProvider();

			provider.ResetAll();
		}

		[Fact]
		public void Should_trigger_changetoken()
		{
			var provider = CreateProvider();
			var hasFired = false;
			provider.GetReloadToken().RegisterChangeCallback(o => hasFired = true, null);

			provider.ResetAll();
			hasFired.Should().BeTrue();
		}

		[Fact]
		public void Should_remove_all_settings()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogLevel.Debug);
			provider.Set("Foo", "Bar");

			provider.ResetAll();
			provider.GetChildKeys(Enumerable.Empty<string>(), null).Should().BeEmpty();
		}
	}
}
