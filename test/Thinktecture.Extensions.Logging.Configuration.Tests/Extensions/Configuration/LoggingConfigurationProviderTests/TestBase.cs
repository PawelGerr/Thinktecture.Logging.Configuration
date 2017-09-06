namespace Thinktecture.Extensions.Configuration.LoggingConfigurationProviderTests
{
	public class TestBase
	{
		protected static LoggingConfigurationProvider CreateProvider(params string[] parentPath)
		{
			return new LoggingConfigurationProvider(parentPath);
		}
	}
}
