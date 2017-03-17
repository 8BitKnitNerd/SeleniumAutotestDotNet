using NUnit.Framework;
using System;

namespace SeleniumAutotestDotNet.Tests {

    [Parallelizable(ParallelScope.Fixtures)]
    public class SearchTests : TestBase {

        [SetUp]
        public void SetUpSearchTest() {
        }

        [TearDown]
        public void TearDownSetUpSearchTest() {
        }

        [Test]
        public void BasicSearch() {
            StartPage.MakeSearch("8-bit knit nerd");
            Assert.IsTrue(SearchResultsPage.PageIsLoaded());
        }

        [Test]
        public void NumberOfSearchHits() {
            int ExpectedNumberOfHits = 1000000;
            StartPage.MakeSearch("8-bit knit nerd");
            Assert.IsTrue(TestHelper.IntInRange(SearchResultsPage.GetNumberOfResults(), Convert.ToInt32(ExpectedNumberOfHits * 0.7), Convert.ToInt32(ExpectedNumberOfHits * 1.3)));
        }

        [Test]
        public void NumberOfHitsOnFirstPage() {
            int ExpectedNumberOfHits = 10;
            StartPage.MakeSearch("8-bit knit nerd");
            Assert.AreEqual(ExpectedNumberOfHits, SearchResultsPage.GetNumberOfHitsOnPage());
        }
    }
}
