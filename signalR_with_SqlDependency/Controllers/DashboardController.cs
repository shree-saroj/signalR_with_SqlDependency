using Microsoft.AspNetCore.Mvc;

namespace signalR_with_SqlDependency.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
