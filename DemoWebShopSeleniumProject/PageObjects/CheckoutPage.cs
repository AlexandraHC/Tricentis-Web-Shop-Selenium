using DemoWebShopSeleniumProject.Tests.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DemoWebShopSeleniumProject.PageObjects
{
    public class CheckoutPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;


        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement TermsAndConditionsBtn => _driver.FindElement(By.Id("termsofservice"));
        public IWebElement CheckoutBtn => _driver.FindElement(By.Id("checkout"));
        public IWebElement CheckoutBillingSteps => _driver.FindElement(By.Id("checkout-steps"));
        public IWebElement TxtFirstName => _driver.FindElement(By.Id("BillingNewAddress_FirstName"));
        public IWebElement TxtLastName => _driver.FindElement(By.Id("BillingNewAddress_LastName"));
        public IWebElement TxtEmail => _driver.FindElement(By.Id("BillingNewAddress_Email"));
        public IWebElement TxtCompany => _driver.FindElement(By.Id("BillingNewAddress_Company"));
        public By BillingCountryDropdownLocator => By.Id("BillingNewAddress_CountryId");
        public SelectElement BillingCountrySelect => new SelectElement(_wait.Until(ExpectedConditions.ElementToBeClickable(BillingCountryDropdownLocator)));
        public By BillingAddressLocator => By.Id("billing-address-select");
        public SelectElement BillingAddressSelect => new SelectElement(_wait.Until(ExpectedConditions.ElementToBeClickable(BillingAddressLocator)));
        public By ShippingCountryDropdownNewAdress => By.Id("shipping-address-select");
        public SelectElement ShippingAddressSelect => new SelectElement(_wait.Until(ExpectedConditions.ElementToBeClickable(ShippingCountryDropdownNewAdress)));
        public IWebElement TxtCity => _driver.FindElement(By.Id("BillingNewAddress_City"));
        public IWebElement TxtFirstAddress => _driver.FindElement(By.Id("BillingNewAddress_Address1"));
        public IWebElement TxtSecondAddress => _driver.FindElement(By.Id("BillingNewAddress_Address2"));
        public IWebElement TxtPostalCode => _driver.FindElement(By.Id("BillingNewAddress_ZipPostalCode"));
        public IWebElement TxtPhoneNumber => _driver.FindElement(By.Id("BillingNewAddress_PhoneNumber"));
        public IWebElement TxtFaxNumber => _driver.FindElement(By.Id("BillingNewAddress_FaxNumber"));
        public IWebElement ContinueBtn => _driver.FindElement(By.ClassName("new-address-next-step-button"));

        public void AcceptTermsAndConditions()
        {
            TermsAndConditionsBtn.Click();
        }

        public void ClickOnCheckoutButton()
        {
            CheckoutBtn.Click();
        }
   
        public void FillInCheckoutForm(string firstName, string lastName, string email, string company, string country, string city,
                      string firstAddresss, string secondAddresss, string postalCode, string phoneNumber, string faxNumber)

        {
            HelperMethods.SendKeysToElement(TxtFirstName, firstName);
            HelperMethods.SendKeysToElement(TxtLastName, lastName);
            HelperMethods.SendKeysToElement(TxtEmail, email);
            HelperMethods.SendKeysToElement(TxtCompany, company);
            BillingCountrySelect.SelectByText(country);
            HelperMethods.SendKeysToElement(TxtCity, city);
            HelperMethods.SendKeysToElement(TxtFirstAddress, firstAddresss);
            HelperMethods.SendKeysToElement(TxtSecondAddress, secondAddresss);
            HelperMethods.SendKeysToElement(TxtPostalCode, postalCode);
            HelperMethods.SendKeysToElement(TxtPhoneNumber, phoneNumber);
            HelperMethods.SendKeysToElement(TxtFaxNumber, faxNumber);
            ContinueBtn.Click();
        }

        public bool AreBillingAddressStepsDisplayed()
        {
            return CheckoutBillingSteps.Displayed;
        }

        public void SelectNewShippingAddressFromDropdown(string address)
        {
            ShippingAddressSelect.SelectByText(address);
        }

        public void SelectNewBillingAddressFromDropdown(string address)
        {
            BillingAddressSelect.SelectByText(address);
        }
    }
}
