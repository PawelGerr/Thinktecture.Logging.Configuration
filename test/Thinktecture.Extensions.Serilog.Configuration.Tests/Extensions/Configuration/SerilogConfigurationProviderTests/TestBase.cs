namespace Thinktecture.Extensions.Configuration.SerilogConfigurationProviderTests
{
	public class TestBase
	{
		protected static SerilogConfigurationProvider CreateProvider(params string[] parentPath)
		{
			return new SerilogConfigurationProvider(parentPath);
		}
	}
}
