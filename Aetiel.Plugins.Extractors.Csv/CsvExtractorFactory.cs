using System;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Extractors.Csv
{
    public class CsvExtractorFactory : IPluginFactory
    {
        public IPlugin Create()
        {
            // We don't actually use params right now. They're just
            // for factory compatibility plus future extension.
            return new CsvExtractor();
        }
    }
}
