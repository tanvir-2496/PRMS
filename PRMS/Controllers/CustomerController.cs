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
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _environment;
        public CustomerController(ApplicationDbContext context, IHttpContextAccessor accessor, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _accessor = accessor;
            _environment = appEnvironment;
        }

        // GET: Customer
        public async Task<IActionResult> Index()
        {
            return View(await _context.CustomerInfo.Where(x => x.IsRemove == false).ToListAsync());
        }

        // GET: Customer/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInfo = await _context.CustomerInfo
                .FirstOrDefaultAsync(m => m.CustomerId == id);
            if (customerInfo == null)
            {
                return NotFound();
            }

            return View(customerInfo);
        }

        // GET: Customer/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CustomerInfo customerInfo)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_environment.WebRootPath, "Images\\CustomerImage");
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var Date = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string ModFileName = Date + ".png";
                            using (var fileStream = new FileStream(Path.Combine(uploads, ModFileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                customerInfo.CustomerImage = ModFileName;
                            }
                        }
                    }
                }
                customerInfo.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                customerInfo.CreatedDate = DateTime.Now;
                customerInfo.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(customerInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customerInfo);
        }

        // GET: Customer/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerInfo = await _context.CustomerInfo.FindAsync(id);
            if (customerInfo == null)
            {
                return NotFound();
            }
            return View(customerInfo);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, CustomerInfo customerInfo)
        {
            if (id != customerInfo.CustomerId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var customerInfoDB = await _context.CustomerInfo.FindAsync(id);
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            var uploads = Path.Combine(_environment.WebRootPath, "Images\\CustomerImage");
                            if (file.Length > 0)
                            {
                                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                var Date = DateTime.Now.ToString("yyyyMMddHHmmss");
                                string ModFileName = Date + ".png";

                                if (ModFileName != null)
                                {
                                    //var _imageDB = _context.InventoryCategory.Where(x => x.InventoryCategoryId == id).Select(x => x.ImageUrl).FirstOrDefault();
                                    //Delete Previous image from folder
                                    if (System.IO.File.Exists(Path.Combine(_environment.WebRootPath, "Images\\CustomerImage") + customerInfoDB.CustomerImage))
                                    {
                                        System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "Images\\CustomerImage") + customerInfoDB.CustomerImage);
                                    }
                                }
                                using (var fileStream = new FileStream(Path.Combine(uploads, ModFileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    customerInfoDB.CustomerImage = ModFileName;
                                }
                            }
                        }
                    }

                    customerInfoDB.FirstName = customerInfo.FirstName;
                    customerInfoDB.LastName = customerInfo.LastName;
                    customerInfoDB.FatherName = customerInfo.FatherName;
                    customerInfoDB.MotherName = customerInfo.MotherName;
                    customerInfoDB.SpouseName = customerInfo.SpouseName;
                    customerInfoDB.NID = customerInfo.NID;
                    customerInfoDB.BirthId = customerInfo.BirthId;
                    customerInfoDB.DateOfBirth = customerInfo.DateOfBirth;
                    customerInfoDB.Contact1 = customerInfo.Contact1;
                    customerInfoDB.Contact2 = customerInfo.Contact2;
                    customerInfoDB.Email = customerInfo.Email;
                    customerInfoDB.PermanentAddress = customerInfo.PermanentAddress;
                    customerInfoDB.PresentAddress = customerInfo.PresentAddress;
                    customerInfoDB.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    customerInfoDB.UpdatedDate = DateTime.Now;
                    _context.Update(customerInfoDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerInfoExists(customerInfo.CustomerId))
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
            return View(customerInfo);
        }

        // GET: Customer/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var customerInfo = await _context.CustomerInfo.FindAsync(id);
            if (id != customerInfo.CustomerId)
            {
                return NotFound();
            }
            try
            {
                customerInfo.IsRemove = true;
                _context.Update(customerInfo);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerInfoExists(customerInfo.CustomerId))
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

        private bool CustomerInfoExists(long id)
        {
            return _context.CustomerInfo.Any(e => e.CustomerId == id);
        }
    }
}
