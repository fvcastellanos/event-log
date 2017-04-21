
using EventLog.Service;
using Microsoft.AspNetCore.Mvc;

namespace EventLog.Controllers
{
    public class EventController : Controller
    {
        private EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult ViewEvents() {

            var map = _eventService.GetEvents();

            return View();
        }
    }
}