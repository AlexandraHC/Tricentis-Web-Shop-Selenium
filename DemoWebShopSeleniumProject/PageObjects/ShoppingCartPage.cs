using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace DemoWebShopSeleniumProject.PageObjects
{
    public class ShoppingCartPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public ShoppingCartPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement ShoppingCartLink => _driver.FindElement(By.LinkText("Shopping cart"));
        public IWebElement HomepageLink => _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("body > div.master-wrapper-page > div.master-wrapper-content > div.header > div.header-logo > a > img")));
        public IEnumerable<IWebElement> Products => _driver.FindElements(By.ClassName("product-item"));
        public IWebElement LaptopProductContainer => Products.FirstOrDefault(p => p.GetAttribute("data-productid") == "31");
        public IWebElement AddLaptopProduct => LaptopProductContainer.FindElement(By.ClassName("product-box-add-to-cart-button"));
        public IWebElement TermsAndConditionsBtn => LaptopProductContainer.FindElement(By.Id("termsofservice"));
        public IWebElement CartItem => _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("cart-item-row")));
        public IWebElement LaptopItem => CartItem.FindElement(By.LinkText("14.1-inch Laptop"));
        public IWebElement RemoveFromCartContainer => _driver.FindElement(By.ClassName("remove-from-cart"));
        public IWebElement RemoveFromCartCheckbox => RemoveFromCartContainer.FindElement(By.Name("removefromcart"));
        public IWebElement EmptyShoppingCartContainer => _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("order-summary-content")));
        public IWebElement ItemQuantity => _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("qty-input")));
        public IWebElement UpdateCartBtn => _driver.FindElement(By.ClassName("update-cart-button"));
        public IWebElement CartProductSubtotal => _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("product-subtotal")));
        public IWebElement LaptopPrice => LaptopProductContainer.FindElement(By.ClassName("actual-price"));
        public By CountryDropdownLocator => By.Id("CountryId");
        public SelectElement CountrySelect => new SelectElement(_driver.FindElement(CountryDropdownLocator));
        public IWebElement TxtPostalCode => _driver.FindElement(By.Id("ZipPostalCode"));
        public IWebElement EstimateShippingBtn => _driver.FindElement(By.Name("estimateshipping"));
        public IWebElement EstimateShippingText => _driver.FindElement(By.ClassName("option-description"));

        // method used to remove products from previous tests from cart 
        private void ClearTheCart()
        {
            foreach (var el in _driver.FindElements(By.Name("removefromcart")))
            {
                el.Click();
            }
            UpdateCart();
        }

        public bool IsShoppingCartEmpty()
        {
            Console.WriteLine("Text: " + EmptyShoppingCartContainer.Text);
            return EmptyShoppingCartContainer != null && 
                string.Equals(EmptyShoppingCartContainer.Text, "Your Shopping Cart is empty!", StringComparison.InvariantCultureIgnoreCase);
        }

        public void NavigateToHompageLink()
        {
            HomepageLink.Click();
        }

        public void AddAProductToCart()
        {
            AddLaptopProduct.Click();  
        }

        public void NavigateToShoppingCartLink()
        {
            ShoppingCartLink.Click();
        }
     
        public bool IsLaptopItemDisplayed()
        {
            return LaptopItem.Displayed;
        }

        public void AcceptTermsAndConditions()
        {
            TermsAndConditionsBtn.Click();
        }

        public void UpdateCart()
        {
            UpdateCartBtn.Click();
        }

        public void RemoveTheItemFromCart()
        {
            RemoveFromCartCheckbox.Click();
            UpdateCart();
        }

        public void InsertAQuantityValue(string input)
        {
            ItemQuantity.Clear();
            ItemQuantity.SendKeys(input);

            var actions = new Actions(_driver);
            actions.SendKeys(Keys.Enter).Perform();
        }

        public double GetLaptopProductPrice()
        {
            return double.Parse(LaptopPrice.Text.Replace(".",","));
        }

        public bool IsCartProductSubtotalUpdated(string expectedValue)
        {
            var cartSubTotalValue = GetCartProductSubtotalUpdatedValue();
            return string.Equals(cartSubTotalValue, expectedValue, StringComparison.CurrentCultureIgnoreCase);
        }

        public void SelectACountryFromDropdown(string text)
        {
            CountrySelect.SelectByText(text);
        }

        public void CompleteThePostalCodeField(string postalCode)
        {
            TxtPostalCode.SendKeys(postalCode);
            EstimateShippingBtn.Click();
        }
        public bool IsTheTextForShippingEstimateDisplayed(string expectedText)
        {
            return string.Equals(EstimateShippingText.Text, expectedText, StringComparison.CurrentCultureIgnoreCase);
        }

        private string GetCartProductSubtotalUpdatedValue()
        {
            string initialValue = CartProductSubtotal.Text;
            var valueUpdated = _wait.Until(drv =>
            {
                string newValue = CartProductSubtotal.Text;
                return newValue != initialValue && !string.IsNullOrEmpty(newValue);
            });

            return CartProductSubtotal.Text;
        }
    }
}
