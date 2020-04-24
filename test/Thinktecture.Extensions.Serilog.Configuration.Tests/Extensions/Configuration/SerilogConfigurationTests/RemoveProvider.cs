using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationTests
{
	public class RemoveProvider : TestBase
	{
		[Fact]
		public void Should_throw_if_provider_is_null()
		{
			CreateConfig(out var collection);
			Action action = () => collection.Remove(null!);

			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_not_raise_errors_if_provider_is_not_in_collection()
		{
			CreateConfig(out var collection);
			collection.Remove(ProviderMock.Object);
		}

		[Fact]
		public void Should_remove_provider_from_internal_collection()
		{
			ProviderMock.Setup(p => p.ResetLevel(null));

			var config = CreateConfig(out var collection);
			collection.Add(ProviderMock.Object);

			config.ResetLevel();
			ProviderMock.Verify(p => p.ResetLevel(null), Times.Once);

			collection.Remove(ProviderMock.Object);

			config.ResetLevel();
			ProviderMock.Verify(p => p.ResetLevel(null), Times.Once);
		}
	}
}
