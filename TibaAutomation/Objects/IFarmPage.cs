using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public interface IFarmPage
    {
        T ChangeContext<T>(IWebDriver driver) where T : class;
        IEditFarmPage ClickOnEditFarmBtn();
        string GetToastMessage();
    }
}