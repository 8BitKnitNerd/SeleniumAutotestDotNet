using SeleniumAutotestDotNet.Supports;

using OpenQA.Selenium;

namespace SeleniumAutotestDotNet.Pages {

    public class BasePage {
        public WebDriver Browser;
        public TestSupport TestHelper = new TestSupport();

        public BasePage() {
        }

        public BasePage(WebDriver inBrowser) {
            this.Browser = inBrowser;
        }

        private IWebElement MainHeading => this.Browser.Driver().FindElement(By.TagName("h1"));

        public string GetMainHeading() {
            return MainHeading.Text;
        }
    }
}