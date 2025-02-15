using DemoWebShopSeleniumProject.Driver;
using DemoWebShopSeleniumProject.PageObjects;
using DemoWebShopSeleniumProject.Tests.Common;

namespace DemoWebShopSeleniumProject.Tests
{
    [TestFixture(DriverType.Firefox)]
    [TestFixture(DriverType.Chrome)]
    [TestFixture(DriverType.Edge)]

    public class CheckoutPageTests : TestBase
    {
        public CheckoutPageTests(DriverType driverType) : base(driverType)
        {
        }

        [SetUp]
        public void Setup()
        {
            base.Setup();
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();
            login.Login("mathytest@example.com", "Math12@");

            ShoppingCartPage shoppingcart = new ShoppingCartPage(_driver);
            shoppingcart.NavigateToHompageLink();
            shoppingcart.AddAProductToCart();
            shoppingcart.NavigateToShoppingCartLink();
        }

        [Test]
        public void CheckoutWithValidCredentials()
        {
            CheckoutPage checkout = new CheckoutPage(_driver);
            checkout.AcceptTermsAndConditions();
            checkout.ClickOnCheckoutButton();
            //checkout.FillInCheckoutForm("Test", "Jhoha", "alexjho@example.com", "WebSolutions", "Sydney", 
             //   "Str.12 C", "Australia", "805960", "0255504321", "+61526");
            //checkout.SelectAcountryFromDropdown("Australia");
        }
    }
}
