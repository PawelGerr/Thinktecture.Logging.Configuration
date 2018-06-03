using System;
using FluentAssertions;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationProviderTests
{
	public class Ctor : TestBase
	{
		[Fact]
		public void Should_throw_if_pathcollection_is_null()
		{
			Action action = () => new SerilogConfigurationProvider(null);
			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_if_pathcollection_contains_null()
		{
			Action action = () => new SerilogConfigurationProvider(new string[] { null });
			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Should_throw_if_pathcollection_contains_segments_with_whitespaces_only()
		{
			Action action = () => new SerilogConfigurationProvider(new[] { " " });
			action.Should().Throw<ArgumentException>();
		}

		[Fact]
		public void Should_not_throw_if_pathcollection_is_empty()
		{
			new SerilogConfigurationProvider(new string[0]);
		}
	}
}
