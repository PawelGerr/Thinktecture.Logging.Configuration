using System;
using FluentAssertions;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationSourceTests
{
	public class Ctor
	{
		[Fact]
		public void Should_throw_if_loggingconfiguration_is_null()
		{
			Action action = () => new LoggingConfigurationSource(null);
			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_if_parentpath_is_null()
		{
			Action action = () => new LoggingConfigurationSource(new LoggingConfiguration(), null);
			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_if_parentpath_contains_null()
		{
			Action action = () => new LoggingConfigurationSource(new LoggingConfiguration(), new string[] { null });
			action.ShouldThrow<ArgumentException>();
		}

		[Fact]
		public void Should_throw_if_parentpath_contains_segments_with_whitespaces_only()
		{
			Action action = () => new LoggingConfigurationSource(new LoggingConfiguration(), " ");
			action.ShouldThrow<ArgumentException>();
		}
	}
}
