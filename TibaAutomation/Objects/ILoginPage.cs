using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public interface ILoginPage
    {
        T ChangeContext<T>(IWebDriver driver) where T : class;
        ILoginPage ClickOnLoginButton();
        ILoginPage LoginPipe(string url, string userName, string password);
        ILoginPage NavigateToUrl(string url);
        ILoginPage SetPassword(string password);
        ILoginPage SetUserName(string userName);
    }
}