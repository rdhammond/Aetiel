using System.Collections.Generic;

namespace Aetiel.Plugins.Interfaces
{
    public interface ILoadPlugin : IPlugin
    {
        bool Transform(IEnumerable<object> tranformed);
    }
}