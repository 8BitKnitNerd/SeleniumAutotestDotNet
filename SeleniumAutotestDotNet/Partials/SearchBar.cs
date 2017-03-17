using SeleniumAutotestDotNet.Supports;

using NUnit.Framework;

using OpenQA.Selenium;

namespace SeleniumAutotestDotNet.Partials {
    public class SearchBar : BasePartial {

        private IWebElement Search => Browser.Driver().FindElement(By.ClassName("tsf"));

        public SearchBar(WebDriver inBrowser) {
            Browser = inBrowser;
        }

        public IWebElement SearchInput => Search.FindElement(By.ClassName("gsfi"));

        public IWebElement SearchSubmitButton => Search.FindElement(By.CssSelector("button[name='btnK']"));

        public IWebElement FeelingLuckyButton => Search.FindElement(By.CssSelector("button[name='btnI']"));

        public void MakeSearchByKeyboard(string searchQuery) {
            TestContext.WriteLine("Searching using keyboard. Query: " + searchQuery);
            SearchInput.Clear();
            SearchInput.SendKeys(searchQuery);
            SearchInput.SendKeys(Keys.Enter);
            Browser.WaitForPageAndAjax();
        }

        public void MakeSearchByMouse(string searchQuery) {
            TestContext.WriteLine("Searching using mouse. Query: " + searchQuery);
            SearchInput.SendKeys(searchQuery);
            SearchSubmitButton.Click();
            Browser.WaitForPageAndAjax();
        }
    }
}
