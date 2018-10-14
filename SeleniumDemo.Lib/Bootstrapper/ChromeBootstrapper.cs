using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using SeleniumDemo.Lib.Bootstrapper;

namespace SeleniumDemo.Lib
{
    public class ChromeBootstrapper : IBootstrapper
    {
        public IWebDriver Bootstrap (string websiteUrl, Uri driverUri) {
            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("--headless");
            chromeOptions.AddArgument("--no-sandbox");

            var driver = new RemoteWebDriver(driverUri, chromeOptions.ToCapabilities());


            driver.Navigate().GoToUrl(websiteUrl);
            return driver;
        }
    }
}
