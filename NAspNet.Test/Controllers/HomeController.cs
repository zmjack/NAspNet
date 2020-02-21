using Microsoft.AspNetCore.Mvc;

namespace NAspNet.Controllers.Test
{
    //[LiveAuthorize]
    public class HomeController : Controller
    {
        public IActionResult Home()
        {
            return Json(new { a = 1 });
        }
    }
}
