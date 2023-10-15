using Microsoft.AspNetCore.Mvc;

namespace eTickets.Web.Controllers
{
    public class ActorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
