using System.Collections.Generic;

namespace Aetiel.Plugins.Interfaces
{
    public interface IExtractPlugin : IPlugin
    {
        ICollection<object> Extract(IPluginParams parameters);
    }
}