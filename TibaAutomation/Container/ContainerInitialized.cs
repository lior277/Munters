using Autofac;
using AutomationInfra.Factory;
using AutomationInfra.Objects;
using OpenQA.Selenium;

namespace AutomationInfra.Container
{
    public class ContainerInitialized
    {
        public IContainer ContainerConfigure(IWebDriver driver)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ApplicationFactory>().As<IApplicationFactory>();

            builder.RegisterType<LoginPage>().As<ILoginPage>()
                .WithParameters(new[] { new TypedParameter(typeof(IWebDriver), driver) });

            builder.RegisterType<EditFarmPage>().As<IEditFarmPage>()
                .WithParameters(new[] { new TypedParameter(typeof(IWebDriver), driver) });

            builder.RegisterType<FarmPage>().As<IFarmPage>()
               .WithParameters(new[] { new TypedParameter(typeof(IWebDriver), driver) });

            builder.RegisterType<MenuItems>().As<IMenuItems>()
               .WithParameters(new[] { new TypedParameter(typeof(IWebDriver), driver) });

            var container = builder.Build();

            return container;
        }
    }
}
