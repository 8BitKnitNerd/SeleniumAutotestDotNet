using System;
using System.Configuration;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;

namespace SeleniumAutotestDotNet.Supports {
	public class TestSupport {

        private Random rand = new Random();

		public int GetRandomInt(int min, int max) {
			return rand.Next(min, max + 1);
		}

        //Returns a random upper-case letter (Swedish alphabet)
	    public string GetRandomLetter() {
	        string alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZÅÄÖ";
	        int randomNumber = GetRandomInt(0, alpha.Length - 1);
	        return alpha.Substring(randomNumber, 1);
	    }

		public int GetIntFromString(string inputString) {
			return Convert.ToInt32(Regex.Match(inputString.Replace(" ", string.Empty), @"\d+").Value);
		}
        
		public string GetImageSourceInElement(IWebElement element) {
			return element.FindElement(By.TagName("img")).GetAttribute("src");
		}

		public bool IntInRange(int testInt, int minValue, int maxValue) {
			if (testInt < minValue) {
				return false;
			}
			if (testInt > maxValue) {
				return false;
			}
			return true;
		}


		public IWebDriver StartBrowser(string browser) {
			Uri testserver = new Uri(ConfigurationManager.ConnectionStrings["SeleniumServerUrl"].ConnectionString);
			IWebDriver driver;
			switch (browser) {
				case Chrome:
					driver = new ChromeDriver();
					break;
				case Firefox:
					FirefoxProfileManager myProfile = new FirefoxProfileManager();
					FirefoxProfile profile = myProfile.GetProfile("default");
					profile.SetPreference("network.cookie.cookieBehavior", 0);
					driver = new FirefoxDriver(profile);
					break;
				case Remotechrome:
					driver = new RemoteWebDriver(testserver, DesiredCapabilities.Chrome());
					break;
				case Remotefx:
					driver = new RemoteWebDriver(testserver, DesiredCapabilities.Firefox());
					break;
				case Remoteie:
					driver = new RemoteWebDriver(testserver, DesiredCapabilities.InternetExplorer());
					break;
				case Remoteunitjs:
					driver = new RemoteWebDriver(testserver, DesiredCapabilities.HtmlUnitWithJavaScript());
					break;
				case Remotephantom:
					driver = new RemoteWebDriver(new Uri(ConfigurationManager.ConnectionStrings["PhantomServerUrl"].ConnectionString), DesiredCapabilities.PhantomJS());
					break;
				case Remoteunit:
					driver = new RemoteWebDriver(DesiredCapabilities.HtmlUnit());
					break;
				case Iexplorer:
					driver = new InternetExplorerDriver();
					break;
				default:
					throw new Exception();
			}
			return driver;
		}
		public const string Firefox = "firefox";
		public const string Iexplorer = "iexplore";
		public const string Chrome = "chrome";
		public const string Remotechrome = "remotechrome";
		public const string Remotefx = "remotefx";
		public const string Remoteie = "remoteie";
		public const string Remoteunitjs = "remoteunitjs";
		public const string Remotephantom = "remotephantom";
		public const string Remoteunit = "remoteunit";
		public const string Internal = "htmlunit";
	}
}
