using System.Web.Mvc;

namespace JonkerBudget.WebApi.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {            
            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
