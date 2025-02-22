using DemoWebShopSeleniumProject.Driver;
using DemoWebShopSeleniumProject.PageObjects;
using DemoWebShopSeleniumProject.Tests.Common;

namespace DemoWebShopSeleniumProject.Tests
{
    [TestFixture(DriverType.Firefox)]
    [TestFixture(DriverType.Chrome)]
    [TestFixture(DriverType.Edge)]

    public class HomepageTest : TestBase
    {
        public HomepageTest(DriverType driverType) : base(driverType)
        {
        }

        [SetUp]
        public void Setup()
        {
            base.Setup();
        }

        [Test]
        public void CheckHomepageTitle()
        {
            Homepage homepage = new Homepage(_driver);
            homepage.ClickOnHomepage();

            var actualTitle = _driver.Title;
            Assert.AreEqual(actualTitle, "Demo Web Shop");
        }

        [Test]
        public void TestSearchBoxInput()
        {
            Homepage homepage = new Homepage(_driver);
            homepage.ClickOnHomepage();

            string expectedText = "bookss";
            homepage.InsertATextOnSearchBox("bookss");
            string actualText = homepage.SearchBox.GetAttribute("value");
            Assert.AreEqual(expectedText, actualText, "The search box text does not match the expected input.");
        }

        [Test]
        public void TestTheResultReturnedBySearchBox()
        {
            var searchText = "computer";
            Homepage homepage = new Homepage(_driver);
            homepage.ClickOnHomepage();
            homepage.EnterATextOnSearchBox(searchText);

            Assert.IsTrue(homepage.IsTheSearchedTextDisplayed(searchText));
        }


        [Test]
        public void TestTheNavigationToDesktopSubcategory()
        {
            Homepage homepage = new Homepage(_driver);
            homepage.ClickOnHomepage();
            homepage.HoverOverComputersAndClickDesktops();

            var expectedUrl = "https://demowebshop.tricentis.com/desktops";
            var actualUrl = _driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl, "Failed to navigate to the Desktops page.");
        }

        [Test]
        public void TestTheNavigationToAProduct()
        {
            Homepage homepage = new Homepage(_driver);
            homepage.ClickOnHomepage();
            homepage.ClickOnComputerProductElement();

            Assert.IsTrue(homepage.IsDisplayedProductDetailsPage());
        }

        [Test]
        public void TestTheNavigationToMyAccountLinkFromFooter()
        {
            Homepage homepage = new Homepage(_driver);
            homepage.ClickOnHomepage();
            homepage.ClickToMyAccountFromFooter();

            Assert.IsTrue(homepage.IsOnWelcomeLoginMessage());
            
        }
    }
}
