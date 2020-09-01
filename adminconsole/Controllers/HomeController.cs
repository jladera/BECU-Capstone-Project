using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using adminconsole.Models;
using adminconsole.Backend;

namespace adminconsole.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HomeBackend backend = new HomeBackend();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// POST: Login
        /// 
        /// FIX ME: SAMPLE LOGIN NOT TO BE USED IN PRODUCTION
        /// </summary>
        [HttpPost, ActionName("Login")]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                return NotFound();
            }

            if (string.IsNullOrEmpty(password))
            {
                return NotFound();
            }

            if (username.Trim() is null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (password.Trim() is null)
            {
                return RedirectToAction(nameof(Index));
            }

            bool result = backend.Login(username, password);

            if (!result)
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction("Index", "Locations");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
