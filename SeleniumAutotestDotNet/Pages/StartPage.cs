using SeleniumAutotestDotNet.Supports;
using SeleniumAutotestDotNet.Partials;

using OpenQA.Selenium;

namespace SeleniumAutotestDotNet.Pages {

    public class StartPage : BasePage {

        private IWebElement Page => Browser.Driver().FindElement(By.ClassName("hp"));

        private IWebElement MainSection => Page.FindElement(By.Id("viewport"));

        public StartPage(WebDriver inBrowser) {
            Browser = inBrowser;
        }

        private SearchBar SearchList => new SearchBar(Browser);

        public void MakeSearch(string searchQuery) {
            SearchList.SearchInput.SendKeys(searchQuery + Keys.Return);
            Browser.WaitForPageAndAjax();
        }

        public bool PageIsLoaded() {
            try {
                if (this.MainSection != null) {
                    return true;
                }
            }
            catch { }
            return false;

        }
    }
}