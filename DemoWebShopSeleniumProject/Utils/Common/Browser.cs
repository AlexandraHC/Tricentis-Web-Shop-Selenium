using OpenQA.Selenium;

namespace DemoWebShopSeleniumProject.Utils.Common;

public class Browser
{
    private IWebDriver _driver;

    public Browser(IWebDriver driver)
    {
        _driver = driver;
    }

    public string GetScreenshot()
    {
        var file = ((ITakesScreenshot)_driver).GetScreenshot();
        var img = file.AsBase64EncodedString;

        return img;
    }
}
