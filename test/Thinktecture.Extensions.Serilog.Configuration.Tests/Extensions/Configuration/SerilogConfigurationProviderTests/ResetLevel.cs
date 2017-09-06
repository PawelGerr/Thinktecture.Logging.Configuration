using FluentAssertions;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationProviderTests
{
	public class ResetLevel : TestBase
	{
		[Fact]
		public void Should_not_raise_errors_if_configuration_key_is_not_present()
		{
			var provider = CreateProvider();

			provider.ResetLevel();
		}

		[Fact]
		public void Should_trigger_changetoken()
		{
			var provider = CreateProvider();
			var hasFired = false;
			provider.GetReloadToken().RegisterChangeCallback(o => hasFired = true, null);

			provider.ResetLevel();
			hasFired.Should().BeTrue();
		}

		[Fact]
		public void Should_remove_loglevel_for_default_category()
		{
			var provider = CreateProvider();
			provider.Set("MinimumLevel:Default", "Debug");
			provider.Set("MinimumLevel", "Debug");

			provider.ResetLevel();
			provider.TryGet("MinimumLevel:Default", out var level).Should().BeFalse();
			provider.TryGet("MinimumLevel", out level).Should().BeFalse();
		}

		[Fact]
		public void Should_not_touch_other_category_as_provided()
		{
			var provider = CreateProvider();
			provider.Set("MinimumLevel:Override:Thinktecture", "Debug");

			provider.ResetLevel();
			provider.TryGet("MinimumLevel:Override:Thinktecture", out var level).Should().BeTrue();
		}

		[Fact]
		public void Should_remove_provided_category()
		{
			var provider = CreateProvider();
			provider.Set("MinimumLevel:Override:Thinktecture", "Debug");

			provider.ResetLevel("Thinktecture");
			provider.TryGet("MinimumLevel:Override:Thinktecture", out var level).Should().BeFalse();
		}
	}
}
