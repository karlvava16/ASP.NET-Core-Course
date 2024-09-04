using Microsoft.AspNetCore.Mvc;

namespace IActionResultExample.Controllers
{
    public class HomeController : Controller
    {
        //Url: /bookstore?bookid=5&isloggedin=true
        [Route("bookstore")]
        public IActionResult Index()
        {
            if (!Request.Query.ContainsKey("bookid"))
            {
                //Response.StatusCode = 400;
                //return Content("Book id is not supplied");
                
                //return new BadRequestResult();
                
                return BadRequest("Book id is not supplied");
            }

            if (string.IsNullOrEmpty(Convert.ToString(Request.Query["bookid"])))
            {
                //Response.StatusCode = 400;
                //return Content("Book id can't be null or empty");
               
                return BadRequest("Book id can't be null or empty");

            }


            int bookId = Convert.ToInt32(ControllerContext.HttpContext.Request.Query["bookid"]);
            if (bookId <= 0)
            {
                Response.StatusCode = 400;
                return Content("Book id can't be less then or equal zero");
            }
            if (bookId > 10000)
            {
                //Response.StatusCode = 404;
                //return Content("Book id can't be greater then 10000");

                return NotFound("Book id can't be greater then 10000");
            }

            if (Convert.ToBoolean(Request.Query["isloggedin"]) == false)
            {
                //Response.StatusCode = 401;
                //return Content("User must be authenticated");

                //return new UnauthorizedResult();

                return Unauthorized("User must be authenticated");
            }

            //return new RedirectToActionResult("Books", "Store", new { });             // 302 - Found
            //return RedirectToAction("Books", "Store", new { id = bookId});            // 302 - Found

            //return new RedirectToActionResult("Books", "Store", new { }, true);       // 301 - Moved Permanently
            //return RedirectToActionPermanent("Books", "Store", new { id = bookId });  // 301 - Moved Permanently

            //return new LocalRedirectResult($"store/books/{bookId}");                  // 302 - Found. Local redirect (work only in your web app)
            //return LocalRedirect($"store/books/{bookId}");                            // 302 - Found. Local redirect (work only in your web app)

            //return new LocalRedirectResult($"store/books/{bookId}", true);            // 301 - Moved Permanently. Local redirect (work only in your web app)
            return LocalRedirectPermanent($"store/books/{bookId}");                     // 301 - Moved Permanently. Local redirect (work only in your web app)

            //return new RedirectResult($"store/books/{bookId}");                       // 302 - Found. For another web sites
            //return Redirect($"store/books/{bookId}");                                 // 302 - Found. For another web sites

            //return new RedirectResult($"store/books/{bookId}", true);                 // 301 - Moved Permanently. For another web sites
            //return RedirectPermanent($"store/books/{bookId}");                        // 301 - Moved Permanently. For another web sites
        }
    }
}
