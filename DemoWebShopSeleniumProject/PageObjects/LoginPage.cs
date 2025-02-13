using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace DemoWebShopSeleniumProject.PageObjects
{
    public class LoginPage
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public IWebElement LoginForm => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("form-fields")));
        public IWebElement TxtEmail => LoginForm.FindElement(By.Id("Email"));
        public IWebElement TxtPassword => LoginForm.FindElement(By.Id("Password"));
        public IWebElement LoginLink => _driver.FindElement(By.ClassName("ico-login"));
        public IWebElement LoginBtn => _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("login-button")));
        public IWebElement LogoutLink => _driver.FindElement(By.LinkText("Log out"));
        public IWebElement RememberMeCheckbox => _driver.FindElement(By.Id("RememberMe"));
        public IWebElement ForgotPasswordLink => _driver.FindElement(By.LinkText("Forgot password?"));
        public IWebElement TxtEmailToRecoveryPassword => _driver.FindElement(By.Id("Email"));
        public IWebElement RecoverPasswordButton => _driver.FindElement(By.Name("send-email"));
        public IEnumerable<IWebElement> ValidationLoginErrors => _driver.FindElements(By.ClassName("validation-summary-errors"));
        public IEnumerable<IWebElement> ValidationLoginEmailError => _driver.FindElements(By.ClassName("field-validation-error"));
        public IEnumerable<IWebElement> ValidationMessageText => _driver.FindElements(By.ClassName("result"));
        public IWebElement EmailLoginValidationError => GetValidationErrorElementByEmail("Email");

        public bool IsMessageEmailTextVisible(string emailTextField)
        {
            var emailValidation = ValidationMessageText.FirstOrDefault();
            if (emailValidation != null)
            {
                return string.Equals(emailValidation.Text, emailTextField, StringComparison.InvariantCultureIgnoreCase);
            }
            return false;
        }

        public IWebElement GetValidationErrorElementByEmail(string field)
        {
            var validationErrorContainer = ValidationLoginEmailError.FirstOrDefault(e => String.Equals(e.GetAttribute("data-valmsg-for"), field, StringComparison.InvariantCultureIgnoreCase));
            if (validationErrorContainer != null)
            {
                return validationErrorContainer.FindElement(By.XPath("./span"));
            }

            return null;
        }

        public IEnumerable<string> GetLoginErrorMessages()
        {
            var messages = new List<string>();

            var el = ValidationLoginErrors.FirstOrDefault();
            if (el != null)
            {
                var firstMessageElement = el.FindElement(By.XPath("./span"));
                if (firstMessageElement != null)
                {
                    messages.Add(firstMessageElement.Text);
                }

                var secondMessageElement = el.FindElement(By.XPath("./ul/li"));
                if (secondMessageElement != null)
                {
                    messages.Add(secondMessageElement.Text);
                }
            }

            return messages;
        }

        public void NavigateToLogin()
        {
            LoginLink.Click();
        }

        public void Login(string email, string password)
        {
            TxtEmail.SendKeys(email);
            TxtPassword.SendKeys(password);
            LoginBtn.Click();
        }

        public bool IsLoggedIn()
        {
            return LogoutLink.Displayed;
        }

        public bool IsLoggedOut()
        {
            return TxtEmail.Displayed && TxtPassword.Displayed;
        }

        public bool IsEmailValidationErrorVisibleWhenEmailIsInvalid()
        {
            return EmailLoginValidationError != null && EmailLoginValidationError.Displayed &&
                string.Equals(EmailLoginValidationError.Text, "Please enter a valid email address.", StringComparison.InvariantCultureIgnoreCase);
        }

        public void NavigateToForgotPasswordLink()
        {
            ForgotPasswordLink.Click();
        }

        public void LoginUsingPasswordRecovery(string email)
        {
            TxtEmailToRecoveryPassword.SendKeys(email);
            RecoverPasswordButton.Click();
        }

        public bool IsEmailValidationErrorVisibleWhenANonExistentEmailWasInserted()
        {
            return EmailLoginValidationError != null && EmailLoginValidationError.Displayed &&
                string.Equals(EmailLoginValidationError.Text, "Wrong email", StringComparison.InvariantCultureIgnoreCase);
        }

        public bool IsEmailValidationErrorVisibleWhenEmailFieldIsEmpty()
        {
            return EmailLoginValidationError != null && EmailLoginValidationError.Displayed &&
                string.Equals(EmailLoginValidationError.Text, "Enter your email", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
