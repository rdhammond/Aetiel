using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Aetiel.Plugins.Interfaces;
using Xunit;

namespace Aetiel.Plugins.Extractors.Csv.Tests
{
    public class CsvExtractorTests
    {
        public class ParamsStub : IPluginParams
        { }

        public class CsvClass1
        {
            public string Test { get; set; }
        }

        public class CsvClass2
        {
            public int Test { get; set; }
        }

        [Fact]
        public void ExtractThrowsExceptionIfWrongParameterType()
        {
            var extractor = new CsvExtractor();
            Assert.Throws<ArgumentException>(() => extractor.Extract(new ParamsStub()));
        }

        [Fact]
        public void ExtractReadsTypedFile() 
        {
            const string PATH = "test.csv";
            const string CONTENT = "ABC";
            try
            {
                File.WriteAllLines(PATH, new[] { "\"Test\"", $"\"{CONTENT}\"" });

                var extractor = new CsvExtractor();
                var extracted = extractor.Extract(new CsvExtractorParams {
                    Files = new[] {
                        new CsvExtractorParams.File(PATH, typeof(CsvClass1))
                    }
                }).ToArray();
                Assert.NotNull(extracted);
                Assert.True(extracted.Length == 1);

                var extractedObj = extracted.First() as CsvClass1;
                Assert.NotNull(extractedObj);
                Assert.Equal(CONTENT, extractedObj.Test);
            }
            finally
            {
                if (File.Exists(PATH))
                {
                    File.Delete(PATH);
                }
            }
        }

        [Fact]
        public void ExtractReadsMultipleTypesFromMultipleFiles()
        {
            var files = new CsvExtractorParams.File[] {
                new CsvExtractorParams.File("test.csv", typeof(CsvClass1)),
                new CsvExtractorParams.File("test2.csv", typeof(CsvClass2))
            };
            var contents = new Dictionary<string,string> {
                { "test.csv", "ABC" },
                { "test2.csv", "123" }
            };
            try
            {
                foreach (var file in files)
                {
                    File.WriteAllLines(file.Path, new[] { "\"Test\"", $"\"{contents[file.Path]}\"" });
                }

                var extractor = new CsvExtractor();
                var extracted = extractor.Extract(new CsvExtractorParams { Files = files }).ToArray();
                Assert.NotNull(extracted);
                Assert.True(extracted.Length == 2);

                var class1Obj = extracted[0] as CsvClass1;
                Assert.NotNull(class1Obj);
                Assert.Equal(contents["test.csv"], class1Obj.Test);

                var class2Obj = extracted[1] as CsvClass2;
                Assert.NotNull(class2Obj);
                Assert.Equal(Convert.ToInt32(contents["test2.csv"]), class2Obj.Test);
            }
            finally
            {
                foreach (var path in files.Select(x => x.Path))
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }
        }
    }
}
