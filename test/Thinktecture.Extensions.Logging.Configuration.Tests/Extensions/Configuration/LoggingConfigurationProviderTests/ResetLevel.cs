using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Thinktecture.Extensions.Configuration.LoggingConfigurationProviderTests
{
   public class ResetLevel : TestBase
   {
      [Fact]
      public void Should_not_raise_errors_if_configuration_key_is_not_present()
      {
         var provider = CreateProvider();

         provider.ResetLevel();
      }

      [Fact]
      public void Should_trigger_changetoken()
      {
         var provider = CreateProvider();
         var hasFired = false;
         provider.GetReloadToken().RegisterChangeCallback(o => hasFired = true, null);

         provider.ResetLevel();
         hasFired.Should().BeTrue();
      }

      [Fact]
      public void Should_remove_loglevel_for_default_category_for_all_providers_using_default_provider()
      {
         var provider = CreateProvider();
         provider.Set("LogLevel:Default", "Debug");

         provider.ResetLevel();
         provider.TryGet("LogLevel:Default", out var level).Should().BeFalse();
      }

      [Fact]
      public void Should_not_touch_other_category_as_provided()
      {
         var provider = CreateProvider();
         provider.Set("LogLevel:Thinktecture", "Debug");

         provider.ResetLevel("OtherCategory");
         provider.TryGet("LogLevel:Thinktecture", out var level).Should().BeTrue();
      }

      [Fact]
      public void Should_not_touch_other_provider_besides_provided_one()
      {
         var provider = CreateProvider();
         provider.Set("Console:LogLevel:Default", "Debug");

         provider.ResetLevel("OtherCategory");
         provider.TryGet("Console:LogLevel:Default", out var level).Should().BeTrue();
      }

      [Fact]
      public void Should_remove_provided_category_for_all_providers()
      {
         var provider = CreateProvider();
         provider.Set("LogLevel:Thinktecture", "Debug");

         provider.ResetLevel("Thinktecture");
         provider.TryGet("LogLevel:Thinktecture", out var level).Should().BeFalse();
      }

      [Fact]
      public void Should_remove_default_category_for_provided_provider()
      {
         var provider = CreateProvider();
         provider.Set("Console:LogLevel:Default", "Debug");

         provider.ResetLevel(null, "Console");
         provider.TryGet("Console:LogLevel:Default", out var level).Should().BeFalse();
      }

      [Fact]
      public void Should_remove_provided_category_for_provided_provider()
      {
         var provider = CreateProvider();
         provider.Set("Console:LogLevel:Thinktecture", "Debug");

         provider.ResetLevel("Thinktecture", "Console");
         provider.TryGet("Console:LogLevel:Thinktecture", out var level).Should().BeFalse();
      }

      [Fact]
      public void Should_not_raise_errors_when_internal_collection_is_empty()
      {
         var provider = CreateProvider();

         provider.ResetLevel();
      }


      [Fact]
      public void Should_remove_all_settings()
      {
         var provider = CreateProvider();
         provider.SetLevel(LogLevel.Debug);
         provider.Set("Foo", "Bar");

         provider.ResetLevel();
         provider.GetChildKeys(Enumerable.Empty<string>(), null).Should().BeEmpty();
      }
   }
}
