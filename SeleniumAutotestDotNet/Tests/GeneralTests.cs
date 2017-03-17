using NUnit.Framework;

namespace SeleniumAutotestDotNet.Tests {

    [Parallelizable(ParallelScope.Fixtures)]
    public class GeneralTests : TestBase {

        [SetUp]
        public void SetUpGeneralTest() {
        }

        [TearDown]
        public void TearDownGeneralTest() {
        }

        [Test]
        public void StartPageLoading() {
            Assert.IsTrue(StartPage.PageIsLoaded());
        }
    }
}