using DemoWebShopSeleniumProject.Driver;
using DemoWebShopSeleniumProject.PageObjects;
using DemoWebShopSeleniumProject.Tests.Common;

namespace DemoWebShopSeleniumProject.Tests
{
    [TestFixture(DriverType.Firefox)]
    [TestFixture(DriverType.Chrome)]
    [TestFixture(DriverType.Edge)]

    public class LoginPageTest : TestBase
    {
        public LoginPageTest(DriverType driverType) : base(driverType)
        {
        }

        [SetUp]
        public void Setup()
        {
            base.Setup();
        }

        //Test method by filling the login form with Valid credentials
        [Test]
        public void LoginFormWithValidCredentials()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();
            login.Login("brasmith@example.com", "BraSmi@@");

            Assert.IsTrue(login.IsLoggedIn());
        }

        //Test method by filling the login form with a non existing user
        [Test]
        public void LoginFormWithANonExistentUser()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();
            login.Login("atest@ecample.com", "Test");

            var errorMessages = login.GetLoginErrorMessages();

            Assert.IsTrue(login.IsLoggedOut());
            Assert.IsTrue(errorMessages.Count() == 2);
            Assert.IsTrue(string.Equals(errorMessages.First(), "Login was unsuccessful. Please correct the errors and try again.", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(string.Equals(errorMessages.Last(), "No customer account found", StringComparison.InvariantCultureIgnoreCase));
        }

        //Test method by leaving all field empty
        [Test]
        public void LoginFormWithAllEmptyFields()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();
            login.Login("", "");

            var errorMessages = login.GetLoginErrorMessages();

            Assert.IsTrue(login.IsLoggedOut());
            Assert.IsTrue(errorMessages.Count() == 2);
            Assert.IsTrue(string.Equals(errorMessages.First(), "Login was unsuccessful. Please correct the errors and try again.", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(string.Equals(errorMessages.Last(), "No customer account found", StringComparison.InvariantCultureIgnoreCase));
        }

        //Test method with an invalid email address
        [Test]
        public void LoginFormWithAnInvalidEmailAddress()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();
            login.Login("@@ test .com", "Test!!@");

            Assert.IsTrue(login.IsLoggedOut());           
            Assert.IsTrue(login.IsEmailValidationErrorVisibleWhenEmailIsInvalid());       
        }

        //Test method by filling login password field with a wrong password
        [Test]
        public void LoginFormWithAWrongPassword()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();
            login.Login("alexjho@example.com", "Jass");

            var errorMessages = login.GetLoginErrorMessages();
           
            Assert.IsTrue(login.IsLoggedOut());
            Assert.IsTrue(errorMessages.Count() == 2);
            Assert.IsTrue(string.Equals(errorMessages.First(), "Login was unsuccessful. Please correct the errors and try again.", StringComparison.InvariantCultureIgnoreCase));
            Assert.IsTrue(string.Equals(errorMessages.Last(), "The credentials provided are incorrect", StringComparison.InvariantCultureIgnoreCase));
        }

        //Test method by fill in the email field for password recovery with a valid email address
        [Test]
        public void LoginByTestingForgotPasswordLinkWithAValidEmail()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();

            login.NavigateToForgotPasswordLink();
            login.LoginUsingPasswordRecovery("alexjho@example.com");

            Assert.IsTrue(login.IsMessageEmailTextVisible("Email with instructions has been sent to you."));
        }

        //Test method by fill in the email field for password recovery with a non-existent email address
        [Test]
        public void LoginByTestingForgotPasswordLinkWithANonExistentEmailAddress()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();

            login.NavigateToForgotPasswordLink();
            login.LoginUsingPasswordRecovery("x.test@example.com");

            Assert.IsTrue(login.IsMessageEmailTextVisible("Email not found."));
        }

        //Test method by fill in the email field for password recovery with an invalid email address
        [Test]
        public void LoginByTestingForgotPasswordLinkWithAnInvalidEmail()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();

            login.NavigateToForgotPasswordLink();
            login.LoginUsingPasswordRecovery("123  @ example.com");
            Assert.IsTrue(login.IsEmailValidationErrorVisibleWhenANonExistentEmailWasInserted());
        }

        //Test method by fill in the email field for password recovery with an empty email address
        [Test]
        public void LoginByTestingForgotPasswordLinkWithAnEmptyField()
        {
            LoginPage login = new LoginPage(_driver);
            login.NavigateToLogin();

            login.NavigateToForgotPasswordLink();
            login.LoginUsingPasswordRecovery("");
            Assert.IsTrue(login.IsEmailValidationErrorVisibleWhenEmailFieldIsEmpty());
        }
    }
}
