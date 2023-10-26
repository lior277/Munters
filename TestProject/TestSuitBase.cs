
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestProject
{
    public class TestSuitBase
    {
        public IWebDriver GetDriver()
        {
            var chromeOptions = new ChromeOptions();

            #region Selenium 4
            chromeOptions.AddArgument("--start-maximized");
            chromeOptions.AddArgument("--no-sandbox");
            chromeOptions.AddArgument("--disable-dev-shm-usage");
            chromeOptions.AddArgument("--ignore-ssl-errors");
            chromeOptions.AddArgument("--disable-browser-side-navigation");
            chromeOptions.AddArgument("--enable-features=NetworkService,NetworkServiceInProcess");
            chromeOptions.AddArgument("--ignore-certificate-errors");

            var cloudOptions = new Dictionary<string, object>();
            cloudOptions.Add("resolution", "1920x1080");
            cloudOptions.Add("ACCEPT_SSL_CERTS", true);
            #endregion

            return new ChromeDriver(chromeOptions);

        }

        public void DriverDispose(IWebDriver? driver = null)
        {
            try
            {
                if (driver != null)
                {
                    driver.Quit();
                }

                Thread.Sleep(10);
                driver = null;
            }
            catch (Exception ex)
            {
                var exceMessage = $"Message: {ex?.Message}";

                throw new Exception(exceMessage);
            }
        }
    }
}