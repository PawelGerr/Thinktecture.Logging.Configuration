using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class AddProvider : TestBase
	{
		private readonly Mock<ILoggingConfigurationProvider> _providerMock;

		public AddProvider()
		{
			_providerMock = new Mock<ILoggingConfigurationProvider>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_throw_if_provider_is_null()
		{
			CreateConfig(out var collection);
			Action action = () => collection.Add(null);

			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_add_provider_to_internal_collection()
		{
			_providerMock.Setup(p => p.ResetAll());

			var config = CreateConfig(out var collection);

			config.ResetAll();
			_providerMock.Verify(p => p.ResetAll(), Times.Never);

			collection.Add(_providerMock.Object);

			config.ResetAll();
			_providerMock.Verify(p => p.ResetAll(), Times.Once);
		}
	}
}
