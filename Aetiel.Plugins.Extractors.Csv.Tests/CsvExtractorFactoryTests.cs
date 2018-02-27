using System;
using Xunit;

namespace Aetiel.Plugins.Extractors.Csv.Tests
{
    public class CsvExtractorFactoryTests
    {
        [Fact]
        public void CreateInstantiatesCsvExtractor()
        {
            var factory = new CsvExtractorFactory();
            var extractor = factory.Create();
            Assert.NotNull(extractor);
            Assert.IsType<CsvExtractor>(extractor);
        }
    }
}
