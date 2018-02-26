using System.Collections.Generic;

namespace Aetiel.Plugins.Interfaces
{
    public interface ILoadPlugin : IPlugin
    {
        bool Transform(ICollection<object> tranformed);
    }
}