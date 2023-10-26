using AutomationInfra.ExtensionsMethods;
using AutomationInfra.Factory;
using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public class LoginPage : ILoginPage
    {
        private IWebDriver _driver;
        private readonly IApplicationFactory _apiFactory;


        public LoginPage(IApplicationFactory apiFactory,
            IWebDriver driver)
        {
            _apiFactory = apiFactory;
            _driver = driver;
        }

        #region Locator's   
        private static By _userNameExp = By.CssSelector("input[id='signInName']");
        private static By _passwordExp = By.CssSelector("input[id='password']");
        private static By _loginBtnExp = By.CssSelector("button[id='next']");
        #endregion Locator's

        public ILoginPage NavigateToUrl(string url)
        {
            _driver.Navigate().GoToUrl(url);

            return this;
        }

        public ILoginPage SetUserName(string userName)
        {
            var element = _driver.SearchElement(_userNameExp);
            element.SendsKeysAuto(_driver, _userNameExp, userName);

            return this;
        }

        public ILoginPage SetPassword(string password)
        {
            _driver.SearchElement(_passwordExp)
                .SendsKeysAuto(_driver, _passwordExp, password);

            return this;
        }

        public ILoginPage ClickOnLoginButton()
        {
            _driver.SearchElement(_loginBtnExp)
                .ForceClick(_driver, _loginBtnExp);

            return this;
        }

        public ILoginPage LoginPipe(string url,
            string userName, string password)
        {

            NavigateToUrl(url);
            SetUserName(userName);
            SetPassword(password);
            ClickOnLoginButton();

            return this;
        }

        public T ChangeContext<T>(IWebDriver driver) where T : class
        {
            return _apiFactory.ChangeContext<T>(_driver);
        }
    }
}
