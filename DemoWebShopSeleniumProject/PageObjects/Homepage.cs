using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DemoWebShopSeleniumProject.PageObjects
{
    public class Homepage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        public Homepage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

        }

        public IWebElement HomepageLink => _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("img[alt='Tricentis Demo Web Shop']")));
        public IWebElement SearchBox => _driver.FindElement(By.Id("small-searchterms"));
        public IWebElement SearchTextBox => _wait.Until(ExpectedConditions.ElementIsVisible(By.Id("Q")));
        public IWebElement ComputersCat => _driver.FindElement(By.XPath("//a[@href='/computers']"));
        public IWebElement DesktopsSubcat => _driver.FindElement(By.XPath("//a[@href='/desktops']"));
        public IWebElement ComputerProductElement =>_driver.FindElement(By.XPath("//a[@title='Show details for Build your own expensive computer']"));
        public IWebElement ComputerProductDetails => _driver.FindElement(By.Id("product-details-form"));
        public IWebElement MyAccountFooterLink => _driver.FindElement(By.XPath("//a[@href='/customer/info']"));
        public IWebElement WelcomeLoginMessage => _driver.FindElement(By.XPath("//h1[text()='Welcome, Please Sign In!']"));

        public void ClickOnHomepage()
        {
            HomepageLink.Click();
        }

        public void InsertATextOnSearchBox(string text)
        {
            SearchBox.Click();
            SearchBox.SendKeys(text);
        }

        public void EnterATextOnSearchBox(string text)
        {
            SearchBox.Click();
            SearchBox.SendKeys(text);
            SearchBox.SendKeys(Keys.Enter); 
        }

        public bool IsTheSearchedTextDisplayed(string text)
        {
            return string.Equals(SearchTextBox.GetAttribute("value"), text, StringComparison.InvariantCultureIgnoreCase);
        }

        public void HoverOverComputersAndClickDesktops()
        {
            Actions actions = new Actions(_driver);
            actions.MoveToElement(ComputersCat).Perform();
            DesktopsSubcat.Click();
        }

        public void ClickOnComputerProductElement()
        {
            ComputerProductElement.Click();
        }

        public bool IsDisplayedProductDetailsPage()
        {
            return ComputerProductDetails.Displayed && ComputerProductDetails != null;
        }

        public void ClickToMyAccountFromFooter()
        {
            MyAccountFooterLink.Click();
        }

        public bool IsOnWelcomeLoginMessage()
        {
            return WelcomeLoginMessage.Displayed && WelcomeLoginMessage != null;
        }
    }
}
