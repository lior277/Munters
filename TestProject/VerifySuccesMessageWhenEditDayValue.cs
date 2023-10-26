using AutomationInfra.Factory;
using AutomationInfra.Objects;
using NUnit.Framework;
using OpenQA.Selenium;

namespace TestProject
{
    [TestFixture]
    public class VerifySuccesMessageWhenEditDayValue : TestSuitBase
    {
        private readonly IApplicationFactory _apiFactory = new ApplicationFactory();
        private readonly string _muntersUrl = "https://qa.www.trioair.net/";
        private readonly string _userName = "Munterstal@gmail.com";
        private readonly string _password = "123456Munters";
        private readonly string _siteName = "FA";
        private readonly string _siteTitle = "1";
        private readonly string _tableNmae = "Temperature Curve";
        private readonly string _expectedToastMessage = "Changes saved";
        private IWebDriver? _driver;

        [SetUp]
        public void SetUp()
        {
            _driver = GetDriver();

            // navigate to youtube page
            _apiFactory
                .ChangeContext<ILoginPage>(_driver)
                .LoginPipe(_muntersUrl, _userName, _password);
        }

        [TearDown]
        public void TearDown()
        {
            try
            {
            }
            finally
            {
                DriverDispose(_driver);
            }
        }

        [Test]
        public void VerifySuccesMessageWhenEditDayValueTest()
        {
            // navigate to temperature curve table
            _apiFactory
                .ChangeContext<IMenuItems>(_driver)
                .ClickOnMainMenuItem(_siteName)
                .ClickOnInnerMenuItem(_siteTitle)
                .ClickOnHamburger()
                .ClickOnMenuTableName(_tableNmae);

            // click on filters button
            _apiFactory
                 .ChangeContext<IFarmPage>(_driver)
                 .ClickOnEditFarmBtn();

            // get toast message
            var actoalToastMessage = _apiFactory
                 .ChangeContext<IEditFarmPage>(_driver)
                 .SetTemperatureCurveDayValuePipe()
                 .ClickOnSaveBtn()
                 .GetToastMessage();

            Assert.True(actoalToastMessage == _expectedToastMessage,
                   $" expected Set Temperature Curve Day Value Message: {_expectedToastMessage}," +
                   $" actual Set Temperature Curve Day Value Message: {actoalToastMessage}");
        }
    }
}