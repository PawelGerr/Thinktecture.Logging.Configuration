using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationSourceTests
{
	public class Build
	{
		private readonly Mock<ILoggingConfigurationProviderCollection> _collectionMock;
		private readonly Mock<IConfigurationBuilder> _builderMock;

		public Build()
		{
			_collectionMock = new Mock<ILoggingConfigurationProviderCollection>(MockBehavior.Loose);
			_builderMock = new Mock<IConfigurationBuilder>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_return_correct_provider()
		{
			var source = new LoggingConfigurationSource(_collectionMock.Object);
			var provider = source.Build(_builderMock.Object);

			provider.Should().BeOfType<LoggingConfigurationProvider>();
		}

		[Fact]
		public void Should_pass_the_prefix_to_provider()
		{
			var source = new LoggingConfigurationSource(_collectionMock.Object, "MyPrefix");
			var provider = (LoggingConfigurationProvider)source.Build(_builderMock.Object);

			provider.SetLevel(LogLevel.Debug);

			provider.TryGet("MyPrefix:LogLevel:Default", out var value).Should().BeTrue();
		}

		[Fact]
		public void Should_add_provider_to_config()
		{
			var source = new LoggingConfigurationSource(_collectionMock.Object, "MyPrefix");
			var provider = (LoggingConfigurationProvider)source.Build(_builderMock.Object);

			_collectionMock.Verify(c => c.Add(provider), Times.Once);
		}
	}
}
