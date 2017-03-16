using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace SeleniumAutotestDotNet.Supports {
	public class WebDriver {
		private readonly IWebDriver driver;
		private TestSupport TestSupport;
		const int MaxdelaySeconds = 20;

		internal WebDriver() {
			this.TestSupport = new TestSupport();
			this.driver = this.TestSupport.StartBrowser(ConfigurationManager.AppSettings["client_browser"]);
            this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Int32.Parse(ConfigurationManager.AppSettings["waiting_time"]));
        }

		public IWebDriver Driver() {
			return this.driver;
		}

		public void ClickUsingJquery(string selector) {
			string script = String.Format("jQuery(\"{0}\").click();", selector);
			TestContext.WriteLine("Script to execute: " + script);
			var javaScriptExecutor = this.driver as IJavaScriptExecutor;
			javaScriptExecutor?.ExecuteScript(script);
		}

		public void ResizeWindow(int width, int height) {
			this.driver.Manage().Window.Size = new Size(width, height);
			Wait(1); //Give time to adjust the size before doing more things
		}

		public void OpenAndResetBrowserSize(string url) {
			this.driver.Navigate().GoToUrl(url);
			ResizeWindow(1024, 768);
            WaitForPageAndAjax();
		}

		public void GoToPage(string url) {
			this.driver.Navigate().GoToUrl(url);
			WaitForPageAndAjax();
		}

		public void WaitForPageAndAjax() {
			WaitForPageToLoad();
			WaitForJqueryAjax();
		}

		public void Wait(int seconds) {
			TestContext.WriteLine("Waiting for: " + seconds + " second(s)");
			Thread.Sleep(1000 * seconds);
		}

		public void WaitForPageToLoad() {
			//TestContext.WriteLine("Waiting for page to load" + Environment.NewLine);
			int delay = MaxdelaySeconds;
			while (delay > 0) {
				var pageIsLoaded = (this.driver as IJavaScriptExecutor).ExecuteScript("return document.readyState").Equals("complete");
				if (pageIsLoaded) {
					break;
				}
				Thread.Sleep(1000);
				delay--;
			}
		}

        public void WaitForJqueryAjax() { 
            int delay = MaxdelaySeconds;
            while (delay > 0)
            {
                Thread.Sleep(1000);
                var jquery = (bool)(this.driver as IJavaScriptExecutor)
                    .ExecuteScript("return window.jQuery == undefined");
                if (jquery)
                {
                    break;
                }
                var ajaxIsComplete = (bool)(this.driver as IJavaScriptExecutor)
                    .ExecuteScript("return window.jQuery.active == 0");
                if (ajaxIsComplete)
                {
                    break;
                }
                delay--;
            }
        }

		public void ScrollElementIntoView(IWebElement element) {
			((IJavaScriptExecutor)this.driver).ExecuteScript("window.scroll(" + element.Location.X + "," + (element.Location.Y - 200) + ");");
            WaitForJqueryAjax();
        }

		internal void SetImplicitWaitToDefault() {
			this.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Int32.Parse(ConfigurationManager.AppSettings["waiting_time"]));
		}

		internal void BrowserBackButton() {
			this.driver.Navigate().Back();
		}

		internal ReadOnlyCollection<LogEntry> CheckBrowserLog() {
            ReadOnlyCollection<LogEntry> logEntries = this.driver.Manage().Logs.GetLog(LogType.Browser);
	        if(logEntries.Count>0) {
                TestContext.WriteLine("Entries in log: " + Environment.NewLine);
                foreach (LogEntry entry in logEntries)
                {
                    TestContext.WriteLine(entry);
                }
            }
	        return logEntries;
	    }

		public int GetScrollPosition() {
			WaitForPageToLoad();
			WaitForJqueryAjax();
			int scrollPosition = int.Parse(((IJavaScriptExecutor)this.driver).ExecuteScript("return window.scrollY;").ToString());
			TestContext.WriteLine("Scrollposition (y): " + scrollPosition);
			return scrollPosition;
		}

		public void TakeScreenshot(string filename = null) {
			if (filename == null) {
				filename = DateTime.Now.ToString("yyyyMMddHHmmss") + ".png";
			}
			Screenshot ss = ((ITakesScreenshot)this.driver).GetScreenshot();
			string path = ConfigurationManager.AppSettings["ScreenshotPath"] + filename;
			ss.SaveAsFile(path, ImageFormat.Png); //use any of the built in image formating
			TestContext.WriteLine("Screenshot saved to: " + path);
		}
	}
}
