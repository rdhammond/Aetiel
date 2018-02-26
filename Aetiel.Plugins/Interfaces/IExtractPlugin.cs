using System.Collections.Generic;

namespace Aetiel.Plugins.Interfaces
{
    public interface IExtractPlugin : IPlugin
    {
        IEnumerable<object> Extract(IPluginParams parameters);
    }
}