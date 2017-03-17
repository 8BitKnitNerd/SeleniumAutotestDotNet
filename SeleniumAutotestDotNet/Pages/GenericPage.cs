using System.Configuration;
using SeleniumAutotestDotNet.Supports;

using OpenQA.Selenium;

namespace SeleniumAutotestDotNet.Pages {

    public class GenericPage : BasePage {
        private IWebElement Page => Browser.Driver().FindElement(By.CssSelector("body"));

        public GenericPage(WebDriver inBrowser) {
            Browser = inBrowser;
        }

        public GenericPage(string url, string type = "OW") {
            string fullUrl = "";
            if (type == "CMS") {
                fullUrl = ConfigurationManager.ConnectionStrings["TestUrlCMS"] + url;
            }
            else {
                fullUrl = ConfigurationManager.ConnectionStrings["TestUrl"] + url;
            }
            Browser.GoToPage(fullUrl);
        }

        private IWebElement FormToggle => Page.FindElement(By.ClassName("contact-form-toggler")).FindElement(By.TagName("a"));

        internal string GetAllText() {
            return Page.Text;
        }
    }
}