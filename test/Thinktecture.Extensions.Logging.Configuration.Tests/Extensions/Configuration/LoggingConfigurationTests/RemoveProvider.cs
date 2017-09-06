using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class RemoveProvider : TestBase
	{
		private readonly Mock<ILoggingConfigurationProvider> _providerMock;

		public RemoveProvider()
		{
			_providerMock = new Mock<ILoggingConfigurationProvider>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_throw_if_provider_is_null()
		{
			CreateConfig(out var collection);
			Action action = () => collection.Remove(null);

			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_not_raise_errors_if_provider_is_not_in_collection()
		{
			CreateConfig(out var collection);
			collection.Remove(_providerMock.Object);
		}

		[Fact]
		public void Should_remove_provider_from_internal_collection()
		{
			_providerMock.Setup(p => p.ResetAll());

			var config = CreateConfig(out var collection);
			collection.Add(_providerMock.Object);

			config.ResetAll();
			_providerMock.Verify(p => p.ResetAll(), Times.Once);

			collection.Remove(_providerMock.Object);

			config.ResetAll();
			_providerMock.Verify(p => p.ResetAll(), Times.Once);
		}
	}
}
