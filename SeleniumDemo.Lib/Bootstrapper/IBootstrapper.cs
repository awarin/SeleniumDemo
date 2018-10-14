using System;
using OpenQA.Selenium;

namespace SeleniumDemo.Lib.Bootstrapper
{
    public interface IBootstrapper
    {
        IWebDriver Bootstrap(string websiteUrl, Uri driverUri);
    }
}
