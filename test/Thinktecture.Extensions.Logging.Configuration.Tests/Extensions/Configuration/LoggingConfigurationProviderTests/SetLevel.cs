using System;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationProviderTests
{
	public class SetLevel : TestBase
	{
		[Fact]
		public void Should_trigger_changetoken()
		{
			var provider = CreateProvider();
			var hasFired = false;
			provider.GetReloadToken().RegisterChangeCallback(o => hasFired = true, null);

			provider.SetLevel(LogLevel.Information);
			hasFired.Should().BeTrue();
		}

		[Fact]
		public void Should_set_loglevel_for_default_category_for_all_providers_using_a_parentpath()
		{
			var provider = CreateProvider("MyLogging");
			provider.SetLevel(LogLevel.Information);

			provider.TryGet("MyLogging:LogLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Information");
		}

		[Fact]
		public void Should_set_loglevel_for_default_category_for_all_providers_using_a_longer_parentpath()
		{
			var provider = CreateProvider("My", "Logging");
			provider.SetLevel(LogLevel.Information);

			provider.TryGet("My:Logging:LogLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Information");
		}

		[Fact]
		public void Should_set_loglevel_for_default_category_for_all_providers_using_no_parentpath()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogLevel.Error);

			provider.TryGet("LogLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Error");
		}

		[Fact]
		public void Should_throw_if_loglevel_is_invalid()
		{
			var provider = CreateProvider();
			Action action = () => provider.SetLevel((LogLevel)42);

			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Should_overwrite_priviously_made_setting()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogLevel.Error);
			provider.SetLevel(LogLevel.Information);

			provider.TryGet("LogLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Information");
		}

		[Fact]
		public void Should_set_loglevel_for_provided_category_for_all_providers()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogLevel.Debug, "Thinktecture");

			provider.TryGet("LogLevel:Thinktecture", out var level).Should().BeTrue();
			level.Should().Be("Debug");
		}

		[Fact]
		public void Should_set_loglevel_for_default_category_for_provided_provider()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogLevel.Debug, null, "Console");

			provider.TryGet("Console:LogLevel:Default", out var level).Should().BeTrue();
			level.Should().Be("Debug");
		}

		[Fact]
		public void Should_set_loglevel_for_provided_category_for_provided_provider()
		{
			var provider = CreateProvider();
			provider.SetLevel(LogLevel.Debug, "Thinktecture", "Console");

			provider.TryGet("Console:LogLevel:Thinktecture", out var level).Should().BeTrue();
			level.Should().Be("Debug");
		}
	}
}
