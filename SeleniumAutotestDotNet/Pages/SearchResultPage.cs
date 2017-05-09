using SeleniumAutotestDotNet.Supports;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;

namespace SeleniumAutotestDotNet.Pages {

    public class SearchResultPage : BasePage {

        private IWebElement Page => Browser.Driver().FindElement(By.ClassName("srp"));

        private IWebElement MainSection => Page.FindElement(By.Id("viewport"));

        private IWebElement SearchResults => Page.FindElement(By.Id("resultStats"));

        private IWebElement PreviousButton => Page.FindElement(By.Id("pnprev"));

        private IWebElement NextButton => Page.FindElement(By.Id("pnnext"));

        private IWebElement CurrentPage => Page.FindElement(By.ClassName("cur"));

        private ReadOnlyCollection<IWebElement> PaginationNumbers => Page.FindElement(By.Id("nav")).FindElements(By.TagName("td"));

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

        public void PaginateNext() {
            NextButton.Click();
            Browser.WaitForPageAndAjax();
        }

        public void PaginatePrevious()
        {
            PreviousButton.Click();
            Browser.WaitForPageAndAjax();
        }

        public void PaginateToNumber(int pageNumber) {
            foreach(IWebElement number in PaginationNumbers) {
                if(number.Text == pageNumber.ToString()) {
                    number.Click();
                    Browser.WaitForPageAndAjax();
                    break;
                }
            }
        }

        public int GetCurrentPage() {
            return TestHelper.GetIntFromString(CurrentPage.Text);
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