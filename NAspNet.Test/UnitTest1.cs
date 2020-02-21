using Microsoft.AspNetCore.Mvc;
using NAspNet.Controllers.Test;
using Xunit;

namespace NAspNet.Test
{
    public class UnitTest1
    {
        private static readonly FakeWeb FakeWeb = new FakeWeb();


        [Fact]
        public void Test1()
        {
            var controller = FakeWeb.CreateController<HomeController>("a123", new[] { "tester" });
            var result = controller.Home();
            var view = (result as JsonResult).Value;

            Assert.Equal("a123", "");
        }
    }
}
