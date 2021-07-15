using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace CreateTableOfRecordsTypeOfT.Tests
{
    internal static class TestCaseSource
    {
        public static IEnumerable<TestCaseData> PersonAccountsSource
        {
            get
            {
                yield return new TestCaseData( new List<PersonTypeForTestsing>() 
                { 
                    new PersonTypeForTestsing() 
                { 
                    Id = 1, 
                    FirstName = "Test1", 
                    LastName = "LastName1", 
                    AccountType = 'W', 
                    Bonuses = 12,
                    DateOfBirth = DateTime.Parse("12.12.2012"),
                    Money = 123
                }, 
                    new PersonTypeForTestsing() 
                { 
                    Id = 2, 
                    FirstName = "Test2", 
                    LastName = "LastName2", 
                    AccountType = 'W', 
                    Bonuses = 455,
                    DateOfBirth = DateTime.Parse("09.10.2003"),
                    Money = 123
                }, 
                    new PersonTypeForTestsing()
                {
                    Id = 3,
                    FirstName = "Test3",
                    LastName = "LastName3",
                    AccountType = 'M',
                    Bonuses = 120,
                    DateOfBirth = DateTime.Parse("1.11.2007"),
                    Money = 5000
                }, 
                    new PersonTypeForTestsing()
                {
                    Id = 4,
                    FirstName = "Test4",
                    LastName = "LastName4",
                    AccountType = 'M',
                    Bonuses = 115,
                    DateOfBirth = DateTime.Parse("03.03.2003"),
                    Money = 455
                }, 
                    new PersonTypeForTestsing()
                {
                    Id = 5,
                    FirstName = "Test5",
                    LastName = "LastName5",
                    AccountType = 'W',
                    Bonuses = 155,
                    DateOfBirth = DateTime.Parse("1.1.2001"),
                    Money = 455
                }
                });
            }
        }
    }
}
