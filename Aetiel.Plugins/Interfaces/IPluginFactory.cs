namespace Aetiel.Plugins.Interfaces
{
    public interface IPluginFactory
    {
        IPlugin Create(IPluginParams pluginParams);
    }
}