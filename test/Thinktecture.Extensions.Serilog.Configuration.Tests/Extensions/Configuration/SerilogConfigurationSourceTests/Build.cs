using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Serilog.Events;
using Xunit;

namespace Thinktecture.Extensions.Configuration.SerilogConfigurationSourceTests
{
	public class Build
	{
		private readonly Mock<ISerilogConfigurationProviderCollection> _collectionMock;
		private readonly Mock<IConfigurationBuilder> _builderMock;

		public Build()
		{
			_collectionMock = new Mock<ISerilogConfigurationProviderCollection>(MockBehavior.Loose);
			_builderMock = new Mock<IConfigurationBuilder>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_return_correct_provider()
		{
			var source = new SerilogConfigurationSource(_collectionMock.Object);
			var provider = source.Build(_builderMock.Object);

			provider.Should().BeOfType<SerilogConfigurationProvider>();
		}

		[Fact]
		public void Should_pass_the_prefix_to_provider()
		{
			var source = new SerilogConfigurationSource(_collectionMock.Object, "MyPrefix");
			var provider = (SerilogConfigurationProvider)source.Build(_builderMock.Object);

			provider.SetLevel(LogEventLevel.Debug);

			provider.TryGet("MyPrefix:MinimumLevel", out var value).Should().BeTrue();
		}

		[Fact]
		public void Should_add_provider_to_config()
		{
			var source = new SerilogConfigurationSource(_collectionMock.Object, "MyPrefix");
			var provider = (SerilogConfigurationProvider)source.Build(_builderMock.Object);

			_collectionMock.Verify(c => c.Add(provider), Times.Once);
		}
	}
}
