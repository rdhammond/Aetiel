using System;
using System.Collections.Generic;
using Aetiel.Plugins.Interfaces;

namespace Aetiel.Plugins.Extractors.Csv
{
    public class CsvExtractorParams : IPluginParams
    {
        public class File
        {
            public string Path { get; }
            public Type ModelType { get; }

            public File(string path, Type modelType)
            {
                Path = path;
                ModelType = modelType;
            }
        }
        
        public IEnumerable<File> Files { get; set; }
    }
}
