using FBCOMSystemManagement.DATA;
using FBCOMSystemManagement.Models;
using FBCOMSystemManagement.VIEWMODEL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FBCOMSystemManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DBContextFBCOM _context;
        public HomeController(DBContextFBCOM context, ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = context;

        }

        public IActionResult Index()
        {
            var installations = _context.Installations.Where(m => m.Installed == "OUI").ToList().Count;
            var telephones = _context.Telephones.Where(m => m.Installed == "NON").ToList().Count;
            var cpes = _context.CPES.Where(m => m.Installed == "NON").ToList().Count;
            HomeStatistics statics = new HomeStatistics()
            {
                instalation = installations.ToString(),
                telephone = telephones.ToString(),
                cpe = cpes.ToString()
             };
            return View(statics);
        }

        public IActionResult Privacy()
        {
            return View();
        }

    
    }
}