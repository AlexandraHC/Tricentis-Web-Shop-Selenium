using DemoWebShopSeleniumProject.Driver;
using DemoWebShopSeleniumProject.Utils.Common;
using DemoWebShopSeleniumProject.Utils;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace DemoWebShopSeleniumProject.Tests.Common;

public class TestBase
{
    protected IWebDriver _driver;
    private readonly DriverType _driverType;
    protected Browser Browser { get; private set; }

    public TestBase(DriverType driverType)
    {
        _driverType = driverType;
    }
    public void Setup()
    {
        ExtentReporting.CreateTest(TestContext.CurrentContext.Test.MethodName + " on " + _driverType.ToString());
        _driver = GetDriverType(_driverType);
        _driver.Navigate().GoToUrl("https://demowebshop.tricentis.com/");
        _driver.Manage().Window.Maximize();
        _driver.Manage().Cookies.DeleteAllCookies();

        Browser = new Browser(_driver);
    }

    //method to get the DriverType
    private IWebDriver GetDriverType(DriverType driverType)
    {    
        switch(driverType)
        {
            case DriverType.Chrome: 
                {
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("--incognito");
                    return new ChromeDriver(chromeOptions);
                    //return new ChromeDriver();
                };
            case DriverType.Firefox: 
                {
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("-private");
                    return new FirefoxDriver(firefoxOptions);
                    //return new FirefoxDriver();
                };
            case DriverType.Edge: 
                {
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.AddArgument("--inprivate");
                    return new EdgeDriver(edgeOptions);
                    //return new EdgeDriver();
                }
            default: return _driver;
        };
    }

    [TearDown]
    public void TearDown()
    {
        EndTest();
        ExtentReporting.EndReporting();

        _driver.Quit();
        _driver.Dispose();
    }

    public void EndTest()
    {
        var testStatus = TestContext.CurrentContext.Result.Outcome.Status;
        var message = TestContext.CurrentContext.Result.Message;

        switch (testStatus)
        {
            case TestStatus.Failed:
                ExtentReporting.LogFail($"Test has failed {message}");
                break;
            case TestStatus.Skipped:
                ExtentReporting.LogInfo($"Test skipped {message}");
                break;
            case TestStatus.Passed:
                ExtentReporting.LogPass($"Test passed {message}");
                break;
            default:
                break;
        }

        ExtentReporting.LogScreenshot("Ending test", Browser.GetScreenshot());
    }
}
