using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public interface IEditFarmPage
    {
        T ChangeContext<T>(IWebDriver driver) where T : class;
        IFarmPage ClickOnSaveBtn();
        List<int> GetTemperatureCurveDayRange();
        IEditFarmPage SetTemperatureCurveDayValue(int value);
        IEditFarmPage SetTemperatureCurveDayValuePipe();
    }
}