using System;
using System.Collections.Generic;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins
{
    public abstract class TransformPlugin : ITransformPlugin
    {
        public IEnumerable<object> Transform(IEnumerable<object> extracted)
        {
            throw new NotImplementedException($"{this.GetType().Name} requires parameters.");
        }

        public abstract IEnumerable<object> Transform(IPluginParams pluginParams, IEnumerable<object> extracted);
    }
}