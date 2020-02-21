using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NAspNet.Controllers.Test;
using Xunit;

namespace NAspNet.Test
{
    public class UnitTest1
    {
        private static readonly FakeWeb FakeWeb = new FakeWeb(services =>
        {
            services.AddScoped<IA, CA>();
            services.AddScoped(x => x.GetService<IA>() as CA);
        });

        public interface IA
        {

        }

        public class CA : IA
        {
        }

        [Fact]
        public void Test1()
        {
            var provider = FakeWeb.BuildServiceProvider();

            var aa1 = provider.GetRequiredService<IA>().GetHashCode();
            var aa2 = provider.GetRequiredService<CA>().GetHashCode();

            var aa3 = FakeWeb.BuildServiceProvider().GetRequiredService<IA>().GetHashCode();
            var aa4 = FakeWeb.BuildServiceProvider().GetRequiredService<CA>().GetHashCode();

            var controller = FakeWeb.CreateController<HomeController>("a123", new[] { "tester" });
            var result = controller.Home();
            var view = (result as JsonResult).Value;


            Assert.Equal("a123", "");
        }
    }
}
