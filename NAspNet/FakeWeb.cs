using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NStandard;
using System;
using System.Linq;
using System.Security.Claims;

namespace NAspNet
{
    public class FakeWeb
    {
        private readonly IServiceCollection Services;
        private ServiceProvider ServiceProvider;

        public FakeWeb()
        {
            Services = new ServiceCollection();
            ServiceProvider = Services.BuildServiceProvider();
        }

        public FakeWeb(IServiceCollection services) : this()
        {
            Services = services;
            ServiceProvider = Services.BuildServiceProvider();
        }

        public FakeWeb(Action<IServiceCollection> services) : this() => ConfigureServices(services);

        public void ConfigureServices(Action<IServiceCollection> configure)
        {
            configure(Services);
            ServiceProvider = Services.BuildServiceProvider();
        }

        public TController CreateController<TController>(string user, string[] roles)
            where TController : Controller
        {
            var constructor = typeof(TController).GetConstructors().First();
            var arguments = constructor.GetParameters().Select(x => ServiceProvider.GetService(x.ParameterType)).ToArray();
            var controller = constructor.Invoke(arguments) as TController;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(ClaimsIdentityEx.Create(user, roles)),
                },
            };
            controller.HttpContext.RequestServices = ServiceProvider;

            return controller;
        }

    }
}
