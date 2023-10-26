using Autofac;
using AutomationInfra.Container;
using OpenQA.Selenium;

namespace AutomationInfra.Factory
{
    public class ApplicationFactory : IApplicationFactory
    {
        public T ChangeContext<T>(IWebDriver? driver = null) where T : class
        {
            //if(typeof(T).Name.ToLower().Contains("api"))
            var containerInit = new ContainerInitialized();
#pragma warning disable CS8604 // Possible null reference argument.
            var container = containerInit.ContainerConfigure(driver);
#pragma warning restore CS8604 // Possible null reference argument.

            return container.Resolve<T>();
        }
    }
}
