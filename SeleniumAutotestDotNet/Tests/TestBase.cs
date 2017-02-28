using System.Configuration;

using SeleniumAutotestDotNet.Pages;
using SeleniumAutotestDotNet.Supports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace SeleniumAutotestDotNet.Tests {
    public class TestBase {

        internal bool Logcheck = true;
        public WebDriver Browser;
        public TestSupport TestHelper;
        public GenericPage GenericPage;
        public StartPage StartPage;
        public SearchResultPage SearchResultsPage;


        [SetUp]
        public void Setup() {
            this.Browser = new WebDriver();
            this.TestHelper = new TestSupport();
            this.Browser.OpenAndResetBrowserSize(ConfigurationManager.ConnectionStrings["SeleniumTestUrl"].ToString());
            this.Browser.WaitForPageAndAjax();
            this.GenericPage = new GenericPage(this.Browser);
            this.StartPage = new StartPage(this.Browser);
            this.SearchResultsPage = new SearchResultPage(this.Browser);
        }


        [TearDown]
        public virtual void TearDown() {
            try {
                if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed) {
                    TestContext.WriteLine("INFO: When test failed, the heading of the page was: " + this.GenericPage.GetMainHeading());
                    this.Browser.TakeScreenshot();
                }
            }
            catch { }
            this.Browser.Driver().Quit();
        }
    }
}
