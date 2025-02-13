using DemoWebShopSeleniumProject.Driver;
using DemoWebShopSeleniumProject.PageObjects;
using DemoWebShopSeleniumProject.PageObjects.Categories;
using DemoWebShopSeleniumProject.Tests.Common;

namespace DemoWebShopSeleniumProject.Tests
{
    [TestFixture(DriverType.Firefox)]
    [TestFixture(DriverType.Chrome)]
    [TestFixture(DriverType.Edge)]

    public class ShoppingCartPageTest : TestBase
    {
        public ShoppingCartPageTest(DriverType driverType) : base(driverType)
        {
        }

        [SetUp]
        public void Setup()
        {
            base.Setup();
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();
            login.Login("alexjho@example.com", "Jass12@");
        }

        [Test]
        public void TestShoppingCart()
        {
            ShoppingCartPage shoppingcart = new ShoppingCartPage(_driver);
            shoppingcart.NavigateToHompageLink();
            shoppingcart.AddAProductToCart();
            shoppingcart.NavigateToShoppingCartLink();
            Assert.IsTrue(shoppingcart.IsLaptopItemDisplayed());
        }

        [Test]
        public void RemoveAnItemFromCart()
        {
            ShoppingCartPage shoppingcart = new ShoppingCartPage(_driver);
            shoppingcart.NavigateToHompageLink();
            shoppingcart.AddAProductToCart();
            shoppingcart.NavigateToShoppingCartLink();
            shoppingcart.RemoveTheItemFromCart();

            Assert.IsTrue(shoppingcart.IsShoppingCartEmpty());
        }

        [Test]
        public void IncreseTheQuantityForAproduct()
        {
            ShoppingCartPage shoppingcart = new ShoppingCartPage(_driver);
            shoppingcart.NavigateToHompageLink();

            var prodQuantity = 3;
            var prodPrice = shoppingcart.GetLaptopProductPrice();

            shoppingcart.AddAProductToCart();
            shoppingcart.NavigateToShoppingCartLink();
            shoppingcart.InsertAQuantityValue(prodQuantity.ToString());
            shoppingcart.UpdateCart();

            string sum = (prodQuantity * prodPrice).ToString("F").Replace(",",".");
            Assert.IsTrue(shoppingcart.IsCartProductSubtotalUpdated(sum));
        }

        [Test]
        public void InsertAdestinationForShippingDelivery()
        {
            ShoppingCartPage shoppingcart = new ShoppingCartPage(_driver);
            shoppingcart.NavigateToHompageLink();
            shoppingcart.AddAProductToCart();
            shoppingcart.NavigateToShoppingCartLink();

            shoppingcart.SelectACountryFromDropdown("Australia");
            shoppingcart.CompleteThePostalCodeField("700555");
            Assert.True(shoppingcart.IsTheTextForShippingEstimateDisplayed("Compared to other shipping methods, like by flight or over seas, ground shipping is carried out closer to the earth"));
        }

        [Test]
        public void AddToCartAGiftCard()
        {
            ShoppingCartPage shoppingcart = new ShoppingCartPage(_driver);
            shoppingcart.NavigateToHompageLink();

            GiftCardsPage giftCard = new GiftCardsPage(_driver);
            giftCard.NavigateToGiftCardCategory();
            giftCard.ClickOnAGiftCardElement();
            giftCard.FillInWithUserPersonalInformations("Alexandra Jhonas ", "alexjho@example.com", "Test");

            shoppingcart.NavigateToShoppingCartLink();
            Assert.IsTrue(giftCard.IsGiftCardDisplayed());
        }

    }
}
