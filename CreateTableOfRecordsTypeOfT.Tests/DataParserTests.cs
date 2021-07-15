using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;

namespace CreateTableOfRecordsTypeOfT.Tests
{
    [TestFixture]
    internal class Tests
    {
        [Test]
        [TestCaseSource(typeof(TestCaseSource), nameof(TestCaseSource.PersonAccountsSource))]
        public void ParseToFile_PersonTypeSequence_WriteTableToFile(IEnumerable<PersonTypeForTestsing> personAccountsSource)
        {
            const string FileName = "PersonAccountData[Test].txt";

            DataParser.ParseToFile(personAccountsSource, FileName);

            Assert.IsTrue(File.Exists(FileName));
        }

        [Test]
        public void ParseToFile_InvalidPath_ThrowArgumentException() => Assert.Throws<ArgumentException>(() => DataParser.ParseToFile(Array.Empty<int>(), string.Empty), "Destination file path can't be null or empty or whitespace");

        [Test]
        public void ParseToFile_NullSource_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() => DataParser.ParseToFile((int[])null, "somePath"), "Source is null");

        [Test]
        public void ParseToConsole_NullSource_ThrowArgumentNullException() => Assert.Throws<ArgumentNullException>(() => DataParser.ParseToConsole((int[])null), "Source is null");
    }
}
