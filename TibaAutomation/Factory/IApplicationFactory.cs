using OpenQA.Selenium;

namespace AutomationInfra.Factory
{
    public interface IApplicationFactory
    {
        T ChangeContext<T>(IWebDriver? driver = null) where T : class;
    }
}