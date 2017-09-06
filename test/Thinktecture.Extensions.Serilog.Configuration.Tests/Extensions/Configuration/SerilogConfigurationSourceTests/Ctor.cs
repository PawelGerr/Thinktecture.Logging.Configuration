using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationSourceTests
{
	public class Ctor
	{
		private readonly Mock<ISerilogConfigurationProviderCollection> _collectionMock;

		public Ctor()
		{
			_collectionMock = new Mock<ISerilogConfigurationProviderCollection>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_throw_if_loggingconfiguration_is_null()
		{
			Action action = () => new SerilogConfigurationSource(null);
			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_if_parentpath_is_null()
		{
			Action action = () => new SerilogConfigurationSource(_collectionMock.Object, null);
			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_if_parentpath_contains_null()
		{
			Action action = () => new SerilogConfigurationSource(_collectionMock.Object, new string[] { null });
			action.ShouldThrow<ArgumentException>();
		}

		[Fact]
		public void Should_throw_if_parentpath_contains_segments_with_whitespaces_only()
		{
			Action action = () => new SerilogConfigurationSource(_collectionMock.Object, " ");
			action.ShouldThrow<ArgumentException>();
		}
	}
}
