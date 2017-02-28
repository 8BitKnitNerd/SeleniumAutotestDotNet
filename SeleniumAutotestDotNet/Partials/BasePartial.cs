using SeleniumAutotestDotNet.Supports;

namespace SeleniumAutotestDotNet.Partials {

	public class BasePartial {
		public WebDriver Browser;

		public BasePartial() {
		}

		public BasePartial(WebDriver inBrowser) {
			this.Browser = inBrowser;
		}
	}
}