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
            int ExpectedNumberOfHits = 1300000;
            StartPage.MakeSearch("8-bit knit nerd");
            Assert.IsTrue(TestHelper.IntInRange(SearchResultsPage.GetNumberOfResults(), Convert.ToInt32(ExpectedNumberOfHits * 0.7), Convert.ToInt32(ExpectedNumberOfHits * 1.3)));
        }

        [Test]
        public void NumberOfHitsOnFirstPage() {
            int ExpectedNumberOfHits = 10;
            StartPage.MakeSearch("8-bit knit nerd");
            Assert.AreEqual(ExpectedNumberOfHits, SearchResultsPage.GetNumberOfHitsOnPage());
        }
        
        [Test]
        public void PaginationNext() {
            StartPage.MakeSearch("8-bit knit nerd");
            for (int i = 2; i < 7; i++) {
                SearchResultsPage.PaginateNext();
                Assert.AreEqual(i, SearchResultsPage.GetCurrentPage());
                Assert.AreEqual(10, SearchResultsPage.GetNumberOfHitsOnPage());
            }
        }

        [Test]
        public void PaginationPrevious() {
            StartPage.MakeSearch("8-bit knit nerd");
            SearchResultsPage.PaginateToNumber(6);
            for (int i = 5; i >= 2; i--) {
                SearchResultsPage.PaginatePrevious();
                Assert.AreEqual(i, SearchResultsPage.GetCurrentPage());
                Assert.AreEqual(10, SearchResultsPage.GetNumberOfHitsOnPage());
            }
        }

        [Test]
        public void NumberPagination() {
            StartPage.MakeSearch("8-bit knit nerd");
            for (int i = 0; i <= 5; i++) {
                int pageToPaginate = TestHelper.GetRandomInt(1, 7);
                SearchResultsPage.PaginateToNumber(pageToPaginate);
                Assert.AreEqual(pageToPaginate, SearchResultsPage.GetCurrentPage());
                Assert.AreEqual(10, SearchResultsPage.GetNumberOfHitsOnPage());
            }
        }
    }
}
