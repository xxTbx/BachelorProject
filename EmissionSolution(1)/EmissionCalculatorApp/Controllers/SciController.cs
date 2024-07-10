using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using EmissionCalculatorApp.Models;

namespace EmissionCalculatorApp.Controllers
{
    public class SciController : Controller
    {
        private readonly ILogger<SciController> _logger;

        public SciController(ILogger<SciController> logger)
        {
            _logger = logger;
        }

        public IActionResult Sci()
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
