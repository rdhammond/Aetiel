using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aetiel.Plugins.Interfaces;
using CsvHelper;

namespace Aetiel.Plugins.Extractors.Csv
{
    public sealed class CsvExtractor : IExtractPlugin
    {
        public IEnumerable<object> Extract(IPluginParams pluginParams)
        {
            var csvParams = pluginParams as CsvExtractorParams;
            if (csvParams == null)
            {
                throw new ArgumentException($"Parameters must be of type ${nameof(CsvExtractorParams)}.");
            }

            var results = new List<object>();
            foreach (var file in csvParams.Files)
            {
                using (var reader = new StreamReader(File.OpenRead(file.Path)))
                using (var csv = new CsvReader(reader))
                {
                    results.AddRange(csv.GetRecords(file.ModelType).ToArray());
                }
            }
            return results;
        }
    }
}