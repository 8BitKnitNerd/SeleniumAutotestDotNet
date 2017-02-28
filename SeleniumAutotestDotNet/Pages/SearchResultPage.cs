using SeleniumAutotestDotNet.Supports;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace SeleniumAutotestDotNet.Pages {

    public class SearchResultPage : BasePage {

        private IWebElement Page => Browser.Driver().FindElement(By.ClassName("srp"));

        private IWebElement MainSection => Page.FindElement(By.Id("viewport"));

        private IWebElement SearchResults => Page.FindElement(By.Id("resultStats"));

        private ReadOnlyCollection<IWebElement> PageResults => Page.FindElements(By.ClassName("g"));

        public SearchResultPage(WebDriver inBrowser) {
            Browser = inBrowser;
        }

        public int GetNumberOfHitsOnPage() {
            return PageResults.Count;
        }

        public int GetNumberOfResults() {
            return TestHelper.GetIntFromString(SearchResults.Text.Split(new string[] { " resultat" }, StringSplitOptions.None)[0]);
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