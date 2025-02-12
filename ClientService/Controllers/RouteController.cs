
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ClientService.Controllers
{
    public class RouteController : Controller
    {
        private readonly ILogger<RouteController> _logger;

        public RouteController(ILogger<RouteController> logger)
        {
            _logger = logger;
        }
        [Route("/receiver")]
        public IActionResult Receiver()
        {
            return View();
        }
        [Route("/sender")]
        public IActionResult Sender()
        {
            return View();
        }
    }
}
