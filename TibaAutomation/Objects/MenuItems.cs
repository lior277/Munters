using AutomationInfra.ExtensionsMethods;
using AutomationInfra.Factory;
using OpenQA.Selenium;

namespace AutomationInfra.Objects
{
    public class MenuItems : IMenuItems
    {
        private readonly static By _FarmFrameExp = By.CssSelector("iframe[class='trio-device-iframe']");
        private string _mainMenue = "//div[@id='site0' and contains(.,'{0}')]";
        private string _innerMenue = "//div[@class='title' and contains(.,'{0}')]";
        private string _menuTableName = "//div[@class='menu-table-name' and contains(.,'{0}')]";
        private IWebDriver _driver;
        private readonly IApplicationFactory _apiFactory;


        public MenuItems(IApplicationFactory apiFactory,
            IWebDriver driver)
        {
            _apiFactory = apiFactory;
            _driver = driver;
        }

        #region Locator's   
        private readonly static By _hamburgerBtnExp = 
            By.CssSelector("button[class='header-item header-button']");
        #endregion Locator's

        public IMenuItems ClickOnMainMenuItem(string siteName)
        {
            var mainMenuItemExp = By.XPath(string.Format(_mainMenue, siteName));

            _driver.SearchElement(mainMenuItemExp)
                .ForceClick(_driver, mainMenuItemExp);

            return this;
        }

        public IMenuItems ClickOnMenuTableName(string tableName)
        {
            var tableNameExp = By.XPath(string.Format(_menuTableName, tableName));

            _driver.SearchElement(tableNameExp)
                .ForceClick(_driver, tableNameExp);

            return this;
        }

        public IMenuItems ClickOnHamburger()
        {
            _driver.SwitchTo().Frame(_driver.SearchElement(_FarmFrameExp));

            _driver.SearchElement(_hamburgerBtnExp)
                .ForceClick(_driver, _hamburgerBtnExp);

            return this;
        }

        public IMenuItems ClickOnInnerMenuItem(string innerMenuName)
        {
            var innerMenuNameExp = By.XPath(string.Format(_innerMenue, innerMenuName));

            _driver.SearchElement(innerMenuNameExp)
                .ForceClick(_driver, innerMenuNameExp);

            return this;
        }

        public T ChangeContext<T>(IWebDriver driver) where T : class
        {
            return _apiFactory.ChangeContext<T>(_driver);
        }
    }
}
