using System;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Extractors.Csv
{
    public class CsvExtractorFactory : IPluginFactory
    {
        public IPlugin Create(IPluginParams pluginParams)
        {
            return new CsvExtractor();
        }
    }
}
