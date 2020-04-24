using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationTests
{
	public class AddProvider : TestBase
	{
		[Fact]
		public void Should_throw_if_provider_is_null()
		{
			CreateConfig(out var collection);
			Action action = () => collection.Add(null!);

			action.Should().Throw<ArgumentNullException>();
		}

		[Fact]
		public void Should_add_provider_to_internal_collection()
		{
			ProviderMock.Setup(p => p.ResetLevel(null));

			var config = CreateConfig(out var collection);

			config.ResetLevel();
			ProviderMock.Verify(p => p.ResetLevel(null), Times.Never);

			collection.Add(ProviderMock.Object);

			config.ResetLevel();
			ProviderMock.Verify(p => p.ResetLevel(null), Times.Once);
		}
	}
}
