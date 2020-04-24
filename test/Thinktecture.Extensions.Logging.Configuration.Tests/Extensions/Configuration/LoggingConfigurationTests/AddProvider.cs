using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
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
			ProviderMock.Setup(p => p.ResetAll());

			var config = CreateConfig(out var collection);

			config.ResetAll();
			ProviderMock.Verify(p => p.ResetAll(), Times.Never);

			collection.Add(ProviderMock.Object);

			config.ResetAll();
			ProviderMock.Verify(p => p.ResetAll(), Times.Once);
		}
	}
}
