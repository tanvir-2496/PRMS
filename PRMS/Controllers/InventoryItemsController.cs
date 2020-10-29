using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PRMS.Data;
using PRMS.Models;

namespace PRMS.Controllers
{
    [Authorize(Roles = "Superadmin, Admin, Manager")]
    public class InventoryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        public InventoryItemsController(ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _context = context;
        }
        // GET: InventoryItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.InventoryItem.Include(i => i.InventoryCategory).Where(x => x.IsRemove ==false);
            return View(await applicationDbContext.Where(x => x.IsRemove == false).ToListAsync());
        }

        // GET: InventoryItems/Create
        public IActionResult Create()
        {
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategory.Where(x => x.IsRemove == false), "InventoryCategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryItem inventoryItem)
        {
            if (ModelState.IsValid)
            {
                inventoryItem.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                inventoryItem.CreatedDate = DateTime.Now;
                inventoryItem.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(inventoryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategory.Where(x => x.IsRemove == false), "InventoryCategoryId", "CategoryName", inventoryItem.InventoryCategoryId);
            return View(inventoryItem);
        }

        // GET: InventoryItems/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inventoryItem = await _context.InventoryItem.FindAsync(id);
            if (inventoryItem == null)
            {
                return NotFound();
            }
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategory.Where(x => x.IsRemove == false), "InventoryCategoryId", "CategoryName", inventoryItem.InventoryCategoryId);
            return View(inventoryItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, InventoryItem inventoryItem)
        {
            if (id != inventoryItem.InventoryItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    inventoryItem.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    inventoryItem.UpdatedDate = DateTime.Now;
                    _context.Update(inventoryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryItemExists(inventoryItem.InventoryItemId))
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
            ViewData["InventoryCategoryId"] = new SelectList(_context.InventoryCategory.Where(x => x.IsRemove == false), "InventoryCategoryId", "CategoryName", inventoryItem.InventoryCategoryId);
            return View(inventoryItem);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            var inventoryItem = await _context.InventoryItem.FindAsync(id);
            if (id != inventoryItem.InventoryItemId)
            {
                return NotFound();
            }
            try
            {
                inventoryItem.IsRemove = true;
                _context.Update(inventoryItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryItemExists(inventoryItem.InventoryItemId))
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

        private bool InventoryItemExists(long id)
        {
            return _context.InventoryItem.Any(e => e.InventoryItemId == id);
        }
    }
}
