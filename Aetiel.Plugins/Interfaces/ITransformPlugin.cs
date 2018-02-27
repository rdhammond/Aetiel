using System.Collections.Generic;

namespace Aetiel.Plugins.Interfaces
{
    public interface ITransformPlugin : IPlugin
    {
        IEnumerable<object> Transform(IEnumerable<object> extracted);
        IEnumerable<object> Transform(IPluginParams pluginParams, IEnumerable<object> extracted);
    }
}