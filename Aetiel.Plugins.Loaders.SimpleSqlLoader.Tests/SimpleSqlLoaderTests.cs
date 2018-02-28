using System;
using System.Data;
using System.Data.Common;
using Xunit;
using Moq;

namespace Aetiel.Plugins.Loaders.SimpleSqlLoader.Tests
{
    public class SimpleSqlLoaderTests
    {
        [Fact]
        public void LoadShouldStoreInTables()
        {
        }

        [Fact]
        public void LoadShouldWorkOnMultipleTables()
        { }

        [Fact]
        public void LoadShouldReturnErrorIfFailed()
        { }

        [Fact]
        public void LoadShouldCommitSuccessAndReturnErrors()
        { }

        [Fact]
        public void LoadShouldThrowExceptionIfFieldsNotInDb()
        { }

        [Fact]
        public void LoadShouldOverrideWithAetielTableNameAttribute()
        { }

        [Fact]
        public void LoadShouldOverrideWithAetielFieldAttribute()
        { }
    }
}
