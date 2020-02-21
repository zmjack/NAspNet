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

        public FakeWeb() => Services = new ServiceCollection();
        public FakeWeb(IServiceCollection services) => Services = services;
        public FakeWeb(Action<IServiceCollection> services) : this() => ConfigureServices(services);

        public void ConfigureServices(Action<IServiceCollection> configure)
        {
            configure(Services);
        }

        public ServiceProvider BuildServiceProvider() => Services.BuildServiceProvider();

        public TController CreateController<TController>(string user, string[] roles)
            where TController : Controller
        {
            var requestServices = BuildServiceProvider();
            var constructor = typeof(TController).GetConstructors().First();
            var arguments = constructor.GetParameters().Select(x => requestServices.GetService(x.ParameterType)).ToArray();
            var controller = constructor.Invoke(arguments) as TController;

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(ClaimsIdentityEx.Create(user, roles)),
                },
            };
            controller.HttpContext.RequestServices = requestServices;

            return controller;
        }

    }
}
