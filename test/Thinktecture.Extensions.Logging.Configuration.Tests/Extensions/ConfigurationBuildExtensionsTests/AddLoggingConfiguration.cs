using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using Thinktecture.Extensions.Configuration;
using Xunit;

namespace Thinktecture.Extensions.ConfigurationBuildExtensionsTests
{
	public class AddLoggingConfiguration
	{
		private readonly Mock<ILoggingConfigurationProviderCollection> _collectionMock;
		private readonly Mock<IConfigurationBuilder> _builderMock;

		public AddLoggingConfiguration()
		{
			_collectionMock = new Mock<ILoggingConfigurationProviderCollection>(MockBehavior.Strict);
			_builderMock = new Mock<IConfigurationBuilder>(MockBehavior.Strict);
		}

		[Fact]
		public void Should_throw_if_builder_is_null()
		{
			Action action = () => ((IConfigurationBuilder)null).AddLoggingConfiguration(_collectionMock.Object);
			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_throw_if_collection_is_null()
		{
			Action action = () => _builderMock.Object.AddLoggingConfiguration(null);
			action.ShouldThrow<ArgumentNullException>();
		}

		[Fact]
		public void Should_add_loggingconfigurationsource()
		{
			IConfigurationSource createSource = null;
			_builderMock.Setup(b => b.Add(It.IsAny<IConfigurationSource>()))
				.Callback<IConfigurationSource>(source => createSource = source)
				.Returns(_builderMock.Object);

			_builderMock.Object.AddLoggingConfiguration(_collectionMock.Object);

			_builderMock.Verify(b => b.Add(It.IsAny<IConfigurationSource>()), Times.Once);
			createSource.Should().BeOfType<LoggingConfigurationSource>();
		}
	}
}
