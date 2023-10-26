using AutomationInfra.ExtensionsMethods;
using AutomationInfra.Factory;
using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public class FarmPage : IFarmPage
    {
        private IWebDriver _driver;
        private readonly IApplicationFactory _apiFactory;


        public FarmPage(IApplicationFactory apiFactory,
            IWebDriver driver)
        {
            _apiFactory = apiFactory;
            _driver = driver;
        }

        #region Locator's   
        private readonly static By _toastMessage = By.CssSelector("div[aria-label='Changes saved']");
        private readonly static By _editBtn = By.CssSelector("div[id='button-edit']");
        #endregion Locator's

        public IEditFarmPage ClickOnEditFarmBtn()
        {
            _driver.SearchElement(_editBtn)
                .ForceClick(_driver, _editBtn);

            return _apiFactory
                .ChangeContext<IEditFarmPage>(_driver);
        }

        public string GetToastMessage()
        {
            return _driver.SearchElement(_toastMessage)
                .GetElementText(_driver, _toastMessage);
        }

        public T ChangeContext<T>(IWebDriver driver) where T : class
        {
            return _apiFactory.ChangeContext<T>(_driver);
        }
    }
}
