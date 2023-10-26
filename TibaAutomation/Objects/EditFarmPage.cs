using AutomationInfra.ExtensionsMethods;
using AutomationInfra.Factory;
using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public class EditFarmPage : IEditFarmPage
    {
        private IWebDriver _driver;
        private readonly IApplicationFactory _apiFactory;


        public EditFarmPage(IApplicationFactory apiFactory,
            IWebDriver driver)
        {
            _apiFactory = apiFactory;
            _driver = driver;
        }

        #region Locator's   
        private readonly static By _temperatureCurveDayFiled = By.CssSelector("input[id='c1_0']");
        private readonly static By _rangeInfo = By.CssSelector("span[class='range-info']");      
        private readonly static By _saveBtn = By.XPath("//div[contains(@class,'label-button') " +
            "and contains(.,'Save')]");
        #endregion Locator's

        public IEditFarmPage SetTemperatureCurveDayValue(int value)
        {
            _driver.SearchElement(_temperatureCurveDayFiled)
                .SendKeys("" + value);

            return this;
        }

        public List<int> GetTemperatureCurveDayRange()
        {
            _driver.SearchElement(_temperatureCurveDayFiled)
                .ForceClick(_driver, _temperatureCurveDayFiled);

            var range = _driver.SearchElement(_rangeInfo)
                 .GetElementText(_driver, _rangeInfo);

            var lowRange = Convert.ToInt32(range
                .Split(" ")
                .First()
                .Trim());

            var highRange = Convert.ToInt32(range
                .Split(" ")
                .Last()
                .Trim());

            return new List<int>() { lowRange, highRange };
        }

        public IEditFarmPage SetTemperatureCurveDayValuePipe()
        {
            var temperatureRange = GetTemperatureCurveDayRange();
            var temperatureValue = Utills.RandomInt(temperatureRange.First(), temperatureRange.Last());
            SetTemperatureCurveDayValue(temperatureValue);

            return this;
        }

        public IFarmPage ClickOnSaveBtn()
        {
            _driver.SearchElement(_saveBtn)
                .ForceClick(_driver, _saveBtn);

            return _apiFactory
                 .ChangeContext<IFarmPage>(_driver);
        }

        public T ChangeContext<T>(IWebDriver driver) where T : class
        {
            return _apiFactory.ChangeContext<T>(_driver);
        }
    }
}
