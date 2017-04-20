using Microsoft.AspNetCore.Mvc;
using EventLog.Model;

namespace EventLog.Controllers
{
    public class LoginController : Controller 
    {
        private EventData _eventData;
        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult Login(string user, string password)
        {
            _eventData = new EventData();

            _eventData.User = user;
            _eventData.Password = password;

            // _eventData.

            return View("Index");
        }
    }
}