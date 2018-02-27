using System.Collections.Generic;

namespace Aetiel.Plugins.Interfaces
{
    public interface ILoadPlugin : IPlugin
    {
        bool Load(IEnumerable<object> tranformed);
        bool Load(IPluginParams pluginParams, IEnumerable<object> transformed);
    }
}