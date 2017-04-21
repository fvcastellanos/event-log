
using Microsoft.AspNetCore.Mvc;

namespace EventLog.Controllers
{
    public class EventController : Controller
    {

        public IActionResult ViewEvents() {
            return View();
        }
    }
}