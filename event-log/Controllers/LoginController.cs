using Microsoft.AspNetCore.Mvc;
using EventLog.Model;
using System;
using EventLog.Service;
using Microsoft.Extensions.Logging;

namespace EventLog.Controllers
{
    public class LoginController : Controller 
    {
        private EventData _eventData;

        private EventService _eventService;

        private CryptoService _cryptoService;

        private ILogger _logger;

        public LoginController(EventService eventService, 
                                CryptoService cryptoService,
                                ILogger<LoginController> logger)
        {
            _eventService = eventService;
            _cryptoService = cryptoService;
            _logger = logger;
        }

        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult HandleLogin(string user, string password)
        {
            _eventData = buildEventData(user, password);
            _eventService.LogEvent(_eventData);

            ViewData["user"] = _eventData.User;

            return View("Welcome");
        }

        private EventData buildEventData(string user, string password)
        {
            EventData eventData = new EventData();

            eventData.User = user;
            eventData.Password = _cryptoService.EncryptTextAes(password);
            eventData.Ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            eventData.UserAgent = _cryptoService.GetSHA256(Request.Headers["User-Agent"]);
            eventData.EventDate = DateTime.Now;

            return eventData;
        }
    }
}