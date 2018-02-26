using System.Collections.Generic;

namespace Aetiel.Plugins.Interfaces
{
    public interface ITransformPlugin : IPlugin
    {
        ICollection<object> Transform(ICollection<object> extracted);
    }
}