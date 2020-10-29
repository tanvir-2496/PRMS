using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
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
    public class CustomerDocumentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _environment;
        public CustomerDocumentController(ApplicationDbContext context, IHttpContextAccessor accessor, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _accessor = accessor;
            _environment = appEnvironment;
        }


        // GET: CustomerDocument
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CustomerDocument.Include(c => c.CustomerInfo).Where(x => x.IsRemove == false);
            return View(await applicationDbContext.Where(x => x.IsRemove == false).ToListAsync());
        }

        // GET: CustomerDocument/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDocument = await _context.CustomerDocument
                .Include(c => c.CustomerInfo)
                .FirstOrDefaultAsync(m => m.CustomerDocumentId == id);
            if (customerDocument == null)
            {
                return NotFound();
            }

            return View(customerDocument);
        }

        // GET: CustomerDocument/Create
        public IActionResult Create()
        {

            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName");
            return View();
        }

        // POST: CustomerDocument/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerDocument customerDocument)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_environment.WebRootPath, "Images\\CustomerDocument");
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            string filetype = Path.GetExtension(file.FileName);                          
                            var Date = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string ModFileName = Date + filetype;
                            using (var fileStream = new FileStream(Path.Combine(uploads, ModFileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                customerDocument.DocumentUrl = ModFileName;
                            }
                        }
                    }
                }
                customerDocument.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customerDocument.CreatedDate = DateTime.Now;
                customerDocument.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(customerDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName", customerDocument.CustomerId);
            return View(customerDocument);
        }

        // GET: CustomerDocument/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerDocument = await _context.CustomerDocument.FindAsync(id);
            if (customerDocument == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName", customerDocument.CustomerId);
            return View(customerDocument);
        }

        // POST: CustomerDocument/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CustomerDocument customerDocument)
        {
            if (id != customerDocument.CustomerDocumentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var customerDocumentDB = await _context.CustomerDocument.FindAsync(id);
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            var uploads = Path.Combine(_environment.WebRootPath, "Images\\CustomerDocument");
                            if (file.Length > 0)
                            {
                                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                string filetype = Path.GetExtension(file.FileName);
                                var Date = DateTime.Now.ToString("yyyyMMddHHmmss");
                                string ModFileName = Date + filetype;

                                if (ModFileName != null)
                                {
                                    //var _imageDB = _context.InventoryCategory.Where(x => x.InventoryCategoryId == id).Select(x => x.ImageUrl).FirstOrDefault();
                                    //Delete Previous image from folder
                                    if (System.IO.File.Exists(Path.Combine(_environment.WebRootPath, "Images\\CustomerDocument") + customerDocumentDB.DocumentUrl))
                                    {
                                        System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "Images\\CustomerDocument") + customerDocumentDB.DocumentUrl);
                                    }
                                }
                                using (var fileStream = new FileStream(Path.Combine(uploads, ModFileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    customerDocumentDB.DocumentUrl = ModFileName;
                                }
                            }
                        }
                    }

                    customerDocumentDB.CustomerId = customerDocument.CustomerId;
                    customerDocumentDB.Remarks = customerDocument.Remarks;
                    customerDocumentDB.ReferanceId = customerDocument.ReferanceId;
                    customerDocumentDB.Referance = customerDocument.Referance;
                    customerDocumentDB.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    customerDocumentDB.UpdatedDate = DateTime.Now;

                    _context.Update(customerDocumentDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerDocumentExists(customerDocument.CustomerDocumentId))
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
            ViewData["CustomerId"] = new SelectList(_context.CustomerInfo, "CustomerId", "FirstName", customerDocument.CustomerId);
            return View(customerDocument);
        }

        // GET: CustomerDocument/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var customerDocument = await _context.CustomerDocument.FindAsync(id);
            if (id != customerDocument.CustomerDocumentId)
            {
                return NotFound();
            }
            try
            {
                customerDocument.IsRemove = true;
                _context.Update(customerDocument);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerDocumentExists(customerDocument.CustomerDocumentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return View(customerDocument);
        }


        private bool CustomerDocumentExists(long id)
        {
            return _context.CustomerDocument.Any(e => e.CustomerDocumentId == id);
        }
    }
}
