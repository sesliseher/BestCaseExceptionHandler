using Microsoft.AspNetCore.Mvc;

namespace BestCaseExceptionHandler.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/Index")]
        public IActionResult Index(int? statusCode = null)
        {
            ViewBag.StatusCode = statusCode;
            return View(); // General eror page
        }
    }
}