using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using NUnit.Framework.Interfaces;

namespace DemoWebShopSeleniumProject.PageObjects.Categories
{
    public class GiftCardsPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;


        public GiftCardsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement GiftCardsCat => _driver.FindElement(By.CssSelector("a[href='/gift-cards']"));
        public IWebElement GiftCardElement => _driver.FindElement(By.LinkText("$5 Virtual Gift Card"));
        public IWebElement TxtRecipientName => _driver.FindElement(By.Id("giftcard_1_RecipientName"));
        public IWebElement TxtRecipientEmail => _driver.FindElement(By.Id("giftcard_1_RecipientEmail"));
        public IWebElement TxtMessage => _driver.FindElement(By.Id("giftcard_1_Message"));
        public IWebElement AddToCartBtn => _driver.FindElement(By.Id("add-to-cart-button-1"));
        public IWebElement AddToWishListBtn => _driver.FindElement(By.Id("add-to-wishlist-button-1"));
        public IWebElement EmailToFriendBtn => _driver.FindElement(By.ClassName("email-a-friend-button"));
        public IWebElement AddToCompareBtn => _driver.FindElement(By.ClassName("add-to-compare-list-button"));
        public IWebElement ItemGiftCartQuantity => _driver.FindElement(By.ClassName("product-name"));

        public void NavigateToGiftCardCategory()
        {
            GiftCardsCat.Click();
        }

        public void ClickOnAGiftCardElement()
        {
            GiftCardElement.Click();
        }

        public void FillInWithUserPersonalInformations(string recipientName, string recipientEmail, string message)
        {
            TxtRecipientName.Clear();
            TxtRecipientEmail.Clear();
            TxtMessage.Clear();

            TxtRecipientName.SendKeys(recipientName);
            TxtRecipientEmail.SendKeys(recipientEmail);
            TxtMessage.SendKeys(message);
            AddToCartBtn.Click();
        }

        public bool IsGiftCardDisplayed()
        {
            return GiftCardElement.Displayed;
        }

    }
}
