using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PRMS.Data;
using PRMS.Models;

namespace PRMS.Controllers
{
    [Authorize(Roles = "Superadmin, Admin, Manager, Staff")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        public HomeController(ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.TotalClient = _context.CustomerInfo.Where(x => x.IsRemove == false).Count();
            ViewBag.TotalShop = _context.InventoryItem.Where(x => x.IsRemove == false).Count();
            ViewBag.TotalHouse = _context.InventoryItem.Where(x => x.IsRemove == false).Count();
            ViewBag.CollectionTotal = _context.Invoice.Where(x => x.IsRemove == false).Sum(x => x.TotalAmount);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
