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
    }
}
