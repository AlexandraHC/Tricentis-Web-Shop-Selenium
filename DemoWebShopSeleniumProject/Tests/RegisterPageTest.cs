using DemoWebShopSeleniumProject.Driver;
using DemoWebShopSeleniumProject.PageObjects;
using DemoWebShopSeleniumProject.Tests.Common;

namespace DemoWebShopSeleniumProject.Tests;

[TestFixture(DriverType.Firefox)]
[TestFixture(DriverType.Chrome)]
[TestFixture(DriverType.Edge)]

public class RegisterPageTest: TestBase
{
    public RegisterPageTest(DriverType driverType) : base(driverType)
    {
    }

    [SetUp]
    public void Setup()
    {
        base.Setup();
    }

    //Test method by filling the register form with Valid credentials
    [Test]
    public void RegisterFormWithValidCredentials()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("Judy", "Smith", "Pass12@@", "Pass12@@");
        register.RegisterBtn.Click();
        Assert.IsTrue(register.IsRegistered());
    }

    //Test method by filling the register form with invalid credentials
    [Test]
    public void RegisterFormWithInvalidCredentials()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("@", "!@#", "a", "@", "@");
        register.RegisterBtn.Click();

        Assert.IsTrue(register.IsEmailValidationErrorVisible());
        Assert.IsTrue(register.IsPasswordValidationErrorVisible());
    }

    //Test method by leaving all fields empty
    [Test]
    public void RegisterFormWithAllFieldsEmpty()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("", "", "", "", "");
        register.RegisterBtn.Click();

        Assert.IsTrue(register.IsFirstNameValidationErrorVisible());
        Assert.IsTrue(register.IsLastNameValidationErrorVisible());
        Assert.IsTrue(register.IsRequiredEmailValidationErrorVisible());
        Assert.IsTrue(register.IsRequiredPasswordValidationErrorVisible());
        Assert.IsTrue(register.IsRequiredConfirmPasswordValidationErrorVisible());
    }

    //Five Testing metode to check every single empty field 
    [Test]
    public void RegisterFormWithFirstNameFieldEmpty()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("", "Harper", "Test12!", "Test12!");
        register.RegisterBtn.Click();

        Assert.IsTrue(register.IsFirstNameValidationErrorVisible());
    }

    [Test]
    public void RegisterFormWithLastNameFieldEmpty()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("Charlie", "", "Teeeest12!", "Teeeest12!");
        register.RegisterBtn.Click();

        Assert.IsTrue(register.IsLastNameValidationErrorVisible());
    }

    [Test]
    public void RegisterFormWithEmailFieldEmpty()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("Charlie", "Test", "", "Teeeest12!", "Teeeest12!");
        register.RegisterBtn.Click();

        Assert.IsTrue(register.IsRequiredEmailValidationErrorVisible());
    }


    [Test]
    public void RegisterFormWithPasswordFieldEmpty()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("Charlie", "Madds", "", "Teeeest12!");
        register.RegisterBtn.Click();

        Assert.IsTrue(register.IsRequiredPasswordValidationErrorVisible());
    }

    [Test]
    public void RegisterFormWithConfirmPasswordFieldEmpty()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("Mady", "Jams", "Teeeest12", "");
        register.RegisterBtn.Click();

        Assert.IsTrue(register.IsRequiredConfirmPasswordValidationErrorVisible());
    }

    //Test method when password confirmation is not the same as password
    [Test]
    public void RegisterFormWithAnotherConfirmationPassword()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("Lily", "Vanden", "Pass", "@");
        register.RegisterBtn.Click();
        Assert.IsTrue(register.IsAWrongConfirmPasswordValidationErrorVisible());
    }

    //Test method my using same email address with another that was already used
    [Test]
    public void RegisterFormWithSameEmailAddress()
    {
        RegisterPage register = new RegisterPage(_driver);
        register.NavigateToRegisterForm();
        register.FillInRegisterForm("Alexandra", "Jhonas", "alexjho@example.com", "Jass12@", "Jass12@");
        register.RegisterBtn.Click();
        Assert.IsTrue(register.IsValidationErrorDisplayedValidationErrorVisible());
    }

}   