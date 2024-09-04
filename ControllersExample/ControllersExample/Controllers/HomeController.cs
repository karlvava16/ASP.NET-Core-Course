using Microsoft.AspNetCore.Mvc;
using ControllersExample.Models;

namespace ControllersExample.Controllers
{
    //[Controller] // use attribute "Controller" to define class as controller
    // or you can just write suffix "Controller" at the end of class name
    public class HomeController : Controller
    {
        [Route("home")]
        [Route("/")]
        public ContentResult Index()
        {
            // if HomeController doesn't inherit from class Controller
            //return new ContentResult() { Content = "Hello from Index", ContentType = "text/plain" };

            // Controller class has method Content that returns ContentResult
            //return Content("Hello from Index", "text/plain");


            return Content("<h1>Welcome</h1> <h2>Hello from Index</h2>", "text/html");
        }

        [Route("about")]
        public string About()
        {
            return "Hello from About";
        }

        [Route("person")]
        public JsonResult Person()
        {
            Person person = new Person()
            {
                Id = Guid.NewGuid(),
                FirstName = "Vladyslav",
                LastName = "Karlinskyi",
                Age = 20
            };

            //return new JsonResult(person);
            return Json(person);
        }

        [Route("contact-us/{mobile:regex(\\d{{10}})}")]
        public string Contact()
        {
            return "Hello from Contact";
        }

        [Route("file-download")]
        public VirtualFileResult FileDownload()
        {
            //return new VirtualFileResult("/sample.pdf", "application/pdf");
            return File("/sample.pdf", "application/pdf");
        }

        [Route("file-download2")]
        public PhysicalFileResult FileDownload2()
        {                                                 //full path
            //return new PhysicalFileResult(@"C:\Step\ASP.NET Core Course\ControllersExample\ControllersExample\wwwroot\Sample.pdf", "application /pdf");
            return PhysicalFile(@"C:\Step\ASP.NET Core Course\ControllersExample\ControllersExample\wwwroot\Sample.pdf", "application /pdf");
        }

        [Route("file-download3")]
        public IActionResult FileDownload3()
        {
            byte[] bytes = System.IO.File.ReadAllBytes(@"C:\Step\ASP.NET Core Course\ControllersExample\ControllersExample\wwwroot\Sample.pdf");
            //return new FileContentResult(bytes, "application/pdf");
            return File(bytes, "application/pdf");
        }
    }
}
