using System;
using OpenQA.Selenium;

namespace SeleniumDemo.Lib.Pages
{
    public class IndexPage : IDisposable
    {
        private readonly IWebDriver _driver;

        public IndexPage (IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement Counter => _driver.FindElement(By.Id("counter__value"));
        public IWebElement IncreaseBtn => _driver.FindElement(By.Id("increase__btn"));
        public IWebElement DecreaseBtn => _driver.FindElement(By.Id("decrease__btn"));

        public string GetWelcomeMessage ()
        {
            return _driver.FindElement(By.XPath("//div[@id='root']/h1")).Text;
        }

        public int GetCounterValue ()
        {
            var valueStr = Counter.Text;
            return int.Parse(valueStr);
        }

        public string GetTabTitle ()
        {
            return _driver.Title;
        }

        public void IncreaseCounterValue () => IncreaseBtn.Click();

        public void DecreaseCounterValue () => DecreaseBtn.Click();

        public void Dispose ()
        {
            _driver.Dispose();
        }
    }
}
