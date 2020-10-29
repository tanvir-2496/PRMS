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
using PRMS.ViewModel;

namespace PRMS.Controllers
{
    [Authorize(Roles = "Superadmin, Admin, Manager")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _environment;

        public InvoiceController(ApplicationDbContext context, IHttpContextAccessor accessor, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _accessor = accessor;
            _environment = appEnvironment;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Invoice.Where(x => x.IsRemove == false).Include(i => i.CustomerInfo).OrderByDescending(x => x.InvoiceId);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invoice/Create
        public IActionResult Create()
        {
            List<InvoiceDetails> ObjInvoice = new List<InvoiceDetails>();
            var customers = _context.CustomerInfo.Where(s => s.IsRemove == false).Select(s => new
            {
                CustomerId = s.CustomerId,
                FirstName = s.FirstName + " " + s.LastName
            }).ToList();
            ViewData["CustomerId"] = new SelectList(customers, "CustomerId", "FirstName");
            return View(ObjInvoice);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromBody] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                invoice.InvoiceNo = GenerateInvoiceNo();
                invoice.IsRemove = false;
                invoice.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                invoice.CreatedDate = DateTime.Now;
                invoice.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(invoice);
                foreach (var details in invoice.InvoiceDetails)
                {
                    //1 = Not Paid, 2 = Install Payed, 3 = Full Paid
                    //if (details.PaidAmount == 0)
                    //{
                    //    details.Status = 1;
                    //}
                    //else if (details.PaidAmount != details.TotalAmount)
                    //{
                    //    details.Status = 2;
                    //}
                    //else if (details.PaidAmount == details.TotalAmount)
                    //{
                    //    details.Status = 3;
                    //}
                    details.IsRemove = false;
                    details.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    details.CreatedDate = DateTime.Now;
                    details.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                    _context.Add(details);
                }

                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("GetInvoice", "Invoice", new { id = invoice.InvoiceId });
                return Json(new { status = "success", id = invoice.InvoiceId });
            }
            return View(invoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FatherName", invoice.CustomerId);
            return View(invoice);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Invoice invoice)
        {
            if (id != invoice.InvoiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceExists(invoice.InvoiceId))
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
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FatherName", invoice.CustomerId);
            return View(invoice);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var invoice = await _context.Invoice.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            invoice.IsRemove = true;
            invoice.UpdatedDate = DateTime.Now;
            invoice.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Update(invoice);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public JsonResult GetItemByCustomerId(Int64 id)
        {
            List<InventoryItem> invItem = new List<InventoryItem>();
            invItem = (from i in _context.InventoryItem
                       join a in _context.Aggrement on i.InventoryItemId equals a.InventoryItemId
                       where a.CustomerId == id && i.IsRemove == false
                       select i).ToList();
            invItem.Insert(0, new InventoryItem { InventoryItemId = 0, ItemName = "-- Select Item --" });
            return Json(new SelectList(invItem, "InventoryItemId", "ItemName"));
        }

        public JsonResult GetAmountDetailsByItemId(Int64 id)
        {
            decimal? MonthlyAmount = _context.Aggrement.FirstOrDefault(x => x.InventoryItemId == id).MonthlyRent;
            return Json(MonthlyAmount);
        }

        private bool InvoiceExists(long id)
        {
            return _context.Invoice.Any(e => e.InvoiceId == id);
        }

        // GET: Invoice
        public async Task<IActionResult> GetInvoice(Int64 id)
        {
            InvoiceVM _invoiceVM = new InvoiceVM();
            ViewBag.Company = _context.Company.FirstOrDefault();
            ViewBag.Invoice = _context.Invoice.Include(x => x.CustomerInfo).FirstOrDefault(x => x.InvoiceId == id);
            ViewBag.InvoiceDetails = _context.InvoiceDetails.Where(x => x.InvoiceId == id).Include(x => x.InventoryItem);
            return View();
        }

        string GenerateInvoiceNo()
        {
            //Ex: INV-2019-000002
            string InvoiceNo = _context.Invoice.Where(s => s.InvoiceNo.Substring(4, 4) == DateTime.Now.Year.ToString()).Max(s => s.InvoiceNo);
            if (InvoiceNo == null)
            {
                InvoiceNo = "INV-" + DateTime.Now.Year + "-" + "00000" + 1;
            }
            else
            {
                string substring = InvoiceNo.Substring(InvoiceNo.Length - 6);
                int value = Convert.ToInt32(substring);
                value = value + 1;
                string AddZero = value.ToString();
                if (AddZero.Length < 2)
                {
                    AddZero = "00000" + value;
                }
                if (AddZero.Length < 3)
                {
                    AddZero = "0000" + value;
                }
                if (AddZero.Length < 4)
                {
                    AddZero = "000" + value;
                }
                if (AddZero.Length < 5)
                {
                    AddZero = "00" + value;
                }
                if (AddZero.Length < 6)
                {
                    AddZero = "0" + value;
                }
                InvoiceNo = "INV-" + DateTime.Now.Year + "-" + (AddZero);
            }
            return InvoiceNo;
        }

    }
}
