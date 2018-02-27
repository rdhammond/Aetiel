using System;
using System.Collections.Generic;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins
{
    public abstract class LoadPlugin : ILoadPlugin
    {
        public bool Load(IEnumerable<object> transformed)
        {
            throw new NotImplementedException($"{this.GetType().Name} requires parameters.");
        }

        public abstract bool Load(IPluginParams pluginParams, IEnumerable<object> transformed);
    }
}