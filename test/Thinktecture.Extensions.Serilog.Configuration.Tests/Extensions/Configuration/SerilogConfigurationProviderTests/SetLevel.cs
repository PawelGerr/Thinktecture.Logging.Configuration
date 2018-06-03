using System;
using FluentAssertions;
using Serilog.Events;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationProviderTests
{
	public class SetLevel : TestBase
	{
		[Fact]
		public void Should_trigger_changetoken()
		{
			var provider = CreateProvider();
			var hasFired = false;
			provider.GetReloadToken().RegisterChangeCallback(o => hasFired = true, null);

			provider.SetLevel(LogEventLevel.Information);
			hasFired.Should().BeTrue();
		}

		[Fact]
		public void Should_set_loglevel_for_default_category_using_a_parentpath()
		{
			var provider = CreateProvider("MyLogging");
			provider.SetLevel(LogEventLevel.Information);

			provider.TryGet("MyLogging:MinimumLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Information");
			provider.TryGet("MyLogging:MinimumLevel", out level).Should().BeTrue();
			level.Should().Be("Information");
		}

		[Fact]
		public void Should_set_loglevel_for_default_category_using_a_longer_parentpath()
		{
			var provider = CreateProvider("My", "Logging");
			provider.SetLevel(LogEventLevel.Information);

			provider.TryGet("My:Logging:MinimumLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Information");
			provider.TryGet("My:Logging:MinimumLevel", out level).Should().BeTrue();
			level.Should().Be("Information");
		}

		[Fact]
		public void Should_set_loglevel_for_default_category_using_no_parentpath()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogEventLevel.Error);

			provider.TryGet("MinimumLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Error");
			provider.TryGet("MinimumLevel", out level).Should().BeTrue();
			level.Should().Be("Error");
		}

		[Fact]
		public void Should_throw_if_loglevel_is_invalid()
		{
			var provider = CreateProvider();
			Action action = () => provider.SetLevel((LogEventLevel)42);

			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Should_overwrite_previously_made_setting()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogEventLevel.Error);
			provider.SetLevel(LogEventLevel.Information);

			provider.TryGet("MinimumLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Information");
			provider.TryGet("MinimumLevel", out level).Should().BeTrue();
			level.Should().Be("Information");
		}

		[Fact]
		public void Should_set_loglevel_for_provided_category()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogEventLevel.Debug, "Thinktecture");

			provider.TryGet("MinimumLevel:Override:Thinktecture", out var level).Should().BeTrue();
			level.Should().Be("Debug");
		}
	}
}
