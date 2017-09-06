using FluentAssertions;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationTests
{
	public class Count : TestBase
	{
		[Fact]
		public void Should_return_0_if_collection_is_empty()
		{
			CreateConfig(out var collection);

			collection.Count.Should().Be(0);
		}

		[Fact]
		public void Should_return_1_after_adding_1_provider()
		{
			CreateConfig(out var collection);

			collection.Add(ProviderMock.Object);
			collection.Count.Should().Be(1);
		}
	}
}
