using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Countries.Parsers;

namespace Countries.Tests
{
    [TestClass]
    public class CsvCountriesFileParserTests
    {
        private ICsvCountrieFileParser CreateTarget()
        {
            return new CsvCountrieFileParser();
        }

        [TestMethod]
        public void WhenReadingAllWeGet245Countries()
        {
            var target = CreateTarget();

            var entities = target.Read(@"Countries.csv");

            Assert.AreEqual(245, entities.Count);
        }
    }
}
