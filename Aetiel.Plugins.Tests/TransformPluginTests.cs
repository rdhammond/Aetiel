using System;
using Xunit;
using Aetiel.Plugins;
using Aetiel.Plugins.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace Aetiel.Plugins.Tests
{
    public class TransformPluginTests
    {
        private class TransformPluginStub : TransformPlugin
        {
            public override IEnumerable<object> Transform(IPluginParams pluginParams, IEnumerable<object> extracted)
            {
                return Enumerable.Empty<object>();
            }
        }

        [Fact]
        public void LoadThrowsExceptionIfNoParamsCtorNotImplemented()
        {
            var plugin = new TransformPluginStub();
            Assert.Throws<NotImplementedException>(() => plugin.Transform(Enumerable.Empty<object>()));
        }
    }
}