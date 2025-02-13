using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace DemoWebShopSeleniumProject.PageObjects;

public class RegisterPage
{
    private IWebDriver _driver;
    private WebDriverWait _wait;

    public RegisterPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
    }

    public IWebElement GenderMaleRadioButton => _driver.FindElement(By.Id("gender-male"));
    public IWebElement GenderFemaleRadioButton => _driver.FindElement(By.Id("gender-female"));
    public IWebElement RegisterLink => _driver.FindElement(By.LinkText("Register"));
    public IWebElement TxtFirstName => _driver.FindElement(By.Id("FirstName"));
    public IWebElement TxtLastName => _driver.FindElement(By.Id("LastName"));
    public IWebElement TxtEmail => _driver.FindElement(By.Id("Email"));
    public IWebElement TxtPassword => _driver.FindElement(By.Id("Password"));
    public IWebElement TxtConfirmingPassword => _driver.FindElement(By.Id("ConfirmPassword"));
    public IWebElement RegisterBtn => _driver.FindElement(By.Id("register-button"));
    public IWebElement RegisterContinueBtn => _wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("register-continue-button")));
    public IWebElement LogoutLink => _driver.FindElement(By.ClassName("ico-logout"));
    public IWebElement UsernameAccount => _driver.FindElement(By.ClassName("account"));
    public IEnumerable<IWebElement> ValidationErrors => _driver.FindElements(By.ClassName("field-validation-error"));
    public IWebElement EmailValidationError => GetValidationErrorElementByField("Email");
    public IWebElement PasswordValidationError => GetValidationErrorElementByField("Password");
    public IWebElement ConfirmPasswordValidationError => GetValidationErrorElementByField("ConfirmPassword");
    public IWebElement FirstNamelValidationError => GetValidationErrorElementByField("FirstName");
    public IWebElement LastNamelValidationError => GetValidationErrorElementByField("LastName");
    public IEnumerable<IWebElement> SameEmailValidationError => _driver.FindElements(By.ClassName("validation-summary-errors"));
    public IWebElement GetValidationErrorElementByEmail(string field)
    {
        var validationSummaryContainer = SameEmailValidationError.FirstOrDefault();
        if (validationSummaryContainer != null)
        {
            return validationSummaryContainer.FindElement(By.XPath("./ul/li"));
        }

        return null;
    }

    public IWebElement GetValidationErrorElementByField(string field)
    {
        var validationErrorContainer = ValidationErrors.FirstOrDefault(e => String.Equals(e.GetAttribute("data-valmsg-for"), field, StringComparison.InvariantCultureIgnoreCase));
        if (validationErrorContainer != null)
        {
            return validationErrorContainer.FindElement(By.XPath("./span"));
        }

        return null;
    }

    public void NavigateToRegisterForm()
    {
        RegisterLink.Click();
    }

    public void FillInRegisterForm(string firstName, string lastName, string email, string password,
        string confirmedPassword)
    {
        GenderFemaleRadioButton.Click();
        TxtFirstName.Clear();
        TxtFirstName.SendKeys(firstName);
        TxtLastName.Clear();
        TxtLastName.SendKeys(lastName);
        TxtEmail.Clear();
        TxtEmail.SendKeys(email);
        TxtPassword.Clear();
        TxtPassword.SendKeys(password);
        TxtConfirmingPassword.Clear();
        TxtConfirmingPassword.SendKeys(confirmedPassword);
    }

    public void FillInRegisterForm(string firstName, string lastName, string password, string confirmedPassword)
    {
        var email_start = DateTime.Now.Ticks;
        var email = email_start + "@example.com";

        GenderFemaleRadioButton.Click();
        TxtFirstName.Clear();
        TxtFirstName.SendKeys(firstName);
        TxtLastName.Clear();
        TxtLastName.SendKeys(lastName);
        TxtEmail.Clear();
        TxtEmail.SendKeys(email);
        TxtPassword.Clear();
        TxtPassword.SendKeys(password);
        TxtConfirmingPassword.Clear();
        TxtConfirmingPassword.SendKeys(confirmedPassword);
    }

    public bool IsRegistered()
    {
        return RegisterContinueBtn.Displayed & LogoutLink.Displayed & UsernameAccount.Displayed;
    }

    public bool IsEmailValidationErrorVisible()
    {
        return EmailValidationError != null && EmailValidationError.Displayed && 
            String.Equals(EmailValidationError.Text, "Wrong email", StringComparison.InvariantCultureIgnoreCase);
    }

    public bool IsPasswordValidationErrorVisible()
    {
        return PasswordValidationError != null && PasswordValidationError.Displayed && 
            String.Equals(PasswordValidationError.Text, "The password should have at least 6 characters.", StringComparison.InvariantCultureIgnoreCase);
    }

    // => Another error message text
    public bool IsFirstNameValidationErrorVisible()
    {
        return FirstNamelValidationError != null && FirstNamelValidationError.Displayed &&
            String.Equals(FirstNamelValidationError.Text, "First name is required.", StringComparison.InvariantCultureIgnoreCase);
    }

    public bool IsLastNameValidationErrorVisible()
    {
        return LastNamelValidationError != null && LastNamelValidationError.Displayed &&
            String.Equals(LastNamelValidationError.Text, "Last name is required.", StringComparison.InvariantCultureIgnoreCase);
    }

    public bool IsRequiredEmailValidationErrorVisible()
    {
        return EmailValidationError != null && EmailValidationError.Displayed &&
            String.Equals(EmailValidationError.Text, "Email is required.", StringComparison.InvariantCultureIgnoreCase);
    }

    public bool IsRequiredPasswordValidationErrorVisible()
    {
        return PasswordValidationError != null && PasswordValidationError.Displayed &&
            String.Equals(PasswordValidationError.Text, "Password is required.", StringComparison.InvariantCultureIgnoreCase);
    }

    public bool IsRequiredConfirmPasswordValidationErrorVisible()
    {
        return ConfirmPasswordValidationError != null && ConfirmPasswordValidationError.Displayed &&
            String.Equals(ConfirmPasswordValidationError.Text, "Password is required.", StringComparison.InvariantCultureIgnoreCase);
    }
    
    public bool IsValidationErrorDisplayedValidationErrorVisible()
    {
        var validationErrorElement = GetValidationErrorElementByEmail("Email");

        return validationErrorElement != null && validationErrorElement.Displayed;
    }

    //Test method when the confirmation password doesn't match.
    public bool IsAWrongConfirmPasswordValidationErrorVisible()
    {
        return ConfirmPasswordValidationError != null && ConfirmPasswordValidationError.Displayed &&
            String.Equals(ConfirmPasswordValidationError.Text, "The password and confirmation password do not match.", StringComparison.InvariantCultureIgnoreCase);
    }
}
