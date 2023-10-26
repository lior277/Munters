using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public interface IMenuItems
    {
        T ChangeContext<T>(IWebDriver driver) where T : class;
        IMenuItems ClickOnHamburger();
        IMenuItems ClickOnInnerMenuItem(string innerMenuName);
        IMenuItems ClickOnMainMenuItem(string siteName);
        IMenuItems ClickOnMenuTableName(string tableName);
    }
}