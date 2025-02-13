using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoWebShopSeleniumProject.Driver;
using DemoWebShopSeleniumProject.PageObjects;
using DemoWebShopSeleniumProject.PageObjects.Categories;
using DemoWebShopSeleniumProject.Tests.Common;

namespace DemoWebShopSeleniumProject.Tests.Categories
{
    [TestFixture(DriverType.Firefox)]
    [TestFixture(DriverType.Chrome)]
    [TestFixture(DriverType.Edge)]

    public class GiftCardPageTest : TestBase
    {
        public GiftCardPageTest(DriverType driverType) : base(driverType)
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
        public void FillInVirtualCardWithUserInformations()
        {
            GiftCardsPage giftCard = new GiftCardsPage(_driver);
            giftCard.NavigateToGiftCardCategory();
            giftCard.ClickOnAGiftCardElement();
            giftCard.FillInWithUserPersonalInformations("Alexandra Jhonas ", "alexjho@example.com", "Test");

            ShoppingCartPage shoppingcart = new ShoppingCartPage(_driver);
            shoppingcart.NavigateToShoppingCartLink();

            Assert.IsTrue(giftCard.IsGiftCardDisplayed());
        }
    }
}
