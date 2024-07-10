using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using EmissionCalculatorApp.Models;

namespace EmissionCalculatorApp.Controllers
{

    public class MethodologyController : Controller
    {
        public IActionResult Methodology()
        {
            return View();
        }
    }
}