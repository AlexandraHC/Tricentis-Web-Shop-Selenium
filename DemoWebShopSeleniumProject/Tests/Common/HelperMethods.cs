using OpenQA.Selenium;

namespace DemoWebShopSeleniumProject.Tests.Common
{
    public static class HelperMethods
    {
        public static void SendKeysToElement(IWebElement element, string text)
        {
            element.Clear();
            element.SendKeys(text);
        }
    }
}
