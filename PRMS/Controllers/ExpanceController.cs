using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRMS.Data;
using PRMS.Models;

namespace PRMS.Controllers
{
    [Authorize(Roles = "Superadmin, Admin, Manager")]
    public class ExpanceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _environment;
        public ExpanceController(ApplicationDbContext context, IHttpContextAccessor accessor, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _accessor = accessor;
            _environment = appEnvironment;
        }

        // GET: Expance
        public async Task<IActionResult> Index()
        {
            var expence = await _context.Expance.Where(x => x.IsRemove == false).OrderByDescending(x => x.CustomerId).ToListAsync();
            foreach (var item in expence)
            {
                if (item.CustomerId.HasValue)
                {
                    item.CustomerName = _context.CustomerInfo.Where(s => s.CustomerId == item.CustomerId).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
                } 
                else if (item.InventoryItemId.HasValue)
                {
                    item.ItemName = _context.InventoryItem.Where(s => s.InventoryItemId == item.InventoryItemId).Select(x => x.ItemName).FirstOrDefault();
                }
            }
            return View(expence);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expance = await _context.Expance
                .FirstOrDefaultAsync(m => m.ExpanceId == id);
            if (expance == null)
            {
                return NotFound();
            }

            return View(expance);
        }

        // GET: Expance/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expance expance)
        {
            if (ModelState.IsValid)
            {
                expance.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                expance.CreatedDate = DateTime.Now;
                expance.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(expance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(expance);
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var expance = await _context.Expance.FindAsync(id);
            if (expance == null)
            {
                return NotFound();
            }
            return View(expance);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Expance expance)
        {
            if (id != expance.ExpanceId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    expance.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    expance.UpdatedDate = DateTime.Now;
                    expance.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    _context.Update(expance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpanceExists(expance.ExpanceId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(expance);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var expance = await _context.Expance.FindAsync(id);
            if (expance == null)
            {
                return NotFound();
            }
            expance.IsRemove = true;
            expance.UpdatedDate = DateTime.Now;
            expance.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Update(expance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpanceExists(long id)
        {
            return _context.Expance.Any(e => e.ExpanceId == id);
        }

        public JsonResult GetExpenceTypeByType(string type)
        {
            if (type == "Customer")
            {
                List<CustomerInfo> customer = new List<CustomerInfo>();
                customer = (from i in _context.CustomerInfo
                           where i.IsRemove == false
                           select new CustomerInfo {
                               CustomerId = i.CustomerId,
                               FirstName = i.FirstName + " " + i.LastName
                           }).ToList();
                customer.Insert(0, new CustomerInfo { CustomerId = 0, FirstName = "-- Select Customer --" });
                return Json(new SelectList(customer, "CustomerId", "FirstName"));
            }
            else if (type == "House")
            {
                List<InventoryItem> invItem = new List<InventoryItem>();
                invItem = (from i in _context.InventoryItem
                           join a in _context.InventoryCategory on i.InventoryItemId equals a.InventoryCategoryId
                           where a.CategoryName == type && i.IsRemove == false
                           select i).ToList();
                invItem.Insert(0, new InventoryItem { InventoryItemId = 0, ItemName = "-- Select Item --" });
                return Json(new SelectList(invItem, "InventoryItemId", "ItemName"));
            }
            else if (type == "Shop")
            {
                List<InventoryItem> invItem = new List<InventoryItem>();
                invItem = (from i in _context.InventoryItem
                           join a in _context.InventoryCategory on i.InventoryItemId equals a.InventoryCategoryId
                           where a.CategoryName == type && i.IsRemove == false
                           select i).ToList();
                invItem.Insert(0, new InventoryItem { InventoryItemId = 0, ItemName = "-- Select Item --" });
                return Json(new SelectList(invItem, "InventoryItemId", "ItemName"));
            }
            return Json(null);
        }
    }
}
