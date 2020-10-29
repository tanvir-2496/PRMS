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
    public class AggrementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        public AggrementController(ApplicationDbContext context, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _context = context;
        }

        // GET: Aggrement
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Aggrement.Include(a => a.CustomerInfo).Include(a => a.InventoryItem).Where(x => x.IsRemove == false);
            return View(await applicationDbContext.Where(x => x.IsRemove == false).ToListAsync());
        }

        // GET: Aggrement/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aggrement = await _context.Aggrement
                .Include(a => a.CustomerInfo)
                .Include(a => a.InventoryItem)
                .FirstOrDefaultAsync(m => m.AggrementId == id);
            if (aggrement == null)
            {
                return NotFound();
            }

            return View(aggrement);
        }

        // GET: Aggrement/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName");
            ViewData["InventoryItemId"] = new SelectList(_context.InventoryItem, "InventoryItemId", "ItemName");
            return View();
        }

        // POST: Aggrement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Aggrement aggrement)
        {
            if (ModelState.IsValid)
            {
                aggrement.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                aggrement.CreatedDate = DateTime.Now;
                aggrement.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(aggrement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName", aggrement.CustomerId);
            ViewData["InventoryItemId"] = new SelectList(_context.InventoryItem, "InventoryItemId", "ItemName", aggrement.InventoryItemId);
            return View(aggrement);
        }

        // GET: Aggrement/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aggrement = await _context.Aggrement.FindAsync(id);
            if (aggrement == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName", aggrement.CustomerId);
            ViewData["InventoryItemId"] = new SelectList(_context.InventoryItem, "InventoryItemId", "ItemName", aggrement.InventoryItemId);
            return View(aggrement);
        }

        // POST: Aggrement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Aggrement aggrement)
        {
            if (id != aggrement.AggrementId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    aggrement.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    aggrement.UpdatedDate = DateTime.Now;
                    _context.Update(aggrement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AggrementExists(aggrement.AggrementId))
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
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName", aggrement.CustomerId);
            ViewData["InventoryItemId"] = new SelectList(_context.InventoryItem, "InventoryItemId", "ItemName", aggrement.InventoryItemId);
            return View(aggrement);
        }

        // GET: Aggrement/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var aggrement = await _context.Aggrement.FindAsync(id);
            if (id != aggrement.AggrementId)
            {
                return NotFound();
            }
            try
            {
                aggrement.IsRemove = true;
                _context.Update(aggrement);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AggrementExists(aggrement.AggrementId))
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


        private bool AggrementExists(long id)
        {
            return _context.Aggrement.Any(e => e.AggrementId == id);
        }
    }
}
