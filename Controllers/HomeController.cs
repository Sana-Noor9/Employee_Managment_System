using System.Diagnostics;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult index_dating()
        {
            return View();
        }
        public IActionResult account_settings()
        {
            return View();
        }


        public IActionResult page_clients()
        {
            return View();
        }
        public IActionResult page_contacts()
        {
            return View();
        }
        public IActionResult calendar_list()
        {
            return View();
        }
        public IActionResult tables_colors()
        {
            return View();
        }
        public IActionResult forms()
        {
            return View();
        }
       
        public IActionResult datatables()
        {
            return View();
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
