using System;
using System.Linq;
using Aetiel.Plugins.Interfaces;
using Xunit;

namespace Aetiel.Plugins.Tests
{
    public class LoadPluginTests
    {
        private class LoadPluginStub : LoadPlugin
        {
            public override bool Load(IPluginParams pluginParams, System.Collections.Generic.IEnumerable<object> transformed)
            {
                return true;
            }
        }

        [Fact]
        public void ThrowsExceptionIfNoParametersCtorNotImplemented()
        {
            var plugin = new LoadPluginStub();
            Assert.Throws<NotImplementedException>(() => plugin.Load(Enumerable.Empty<object>()));
        }
    }
}