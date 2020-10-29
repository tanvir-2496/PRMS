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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using PRMS.Data;
using PRMS.Models;

namespace PRMS.Controllers
{
    [Authorize(Roles = "Superadmin, Admin, Manager")]
    public class CategorieController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHttpContextAccessor _accessor;
        private readonly IHostingEnvironment _environment;
        public CategorieController(ApplicationDbContext context, IHttpContextAccessor accessor, IHostingEnvironment appEnvironment)
        {
            _context = context;
            _accessor = accessor;
            _environment = appEnvironment;
        }

        // GET: Categorie
        public async Task<IActionResult> Index()
        {
            return View(await _context.InventoryCategory.Where(x => x.IsRemove == false).ToListAsync());
        }

        // GET: Categorie/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InventoryCategory inventoryCategory)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_environment.WebRootPath, "Images\\Categorys");
                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                            var Date = DateTime.Now.ToString("yyyyMMddHHmmss");
                            string ModFileName = Date + ".png";
                            using (var fileStream = new FileStream(Path.Combine(uploads, ModFileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                inventoryCategory.ImageUrl = ModFileName;
                            }
                        }
                    }
                }
                inventoryCategory.CreatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                inventoryCategory.CreatedDate = DateTime.Now;
                inventoryCategory.UserIp = _accessor.HttpContext.Connection.RemoteIpAddress.ToString();
                _context.Add(inventoryCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventoryCategory);
        }

        // GET: Categorie/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var inventoryCategory = await _context.InventoryCategory.FindAsync(id);
            if (inventoryCategory == null)
            {
                return NotFound();
            }
            return View(inventoryCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, InventoryCategory inventoryCategory)
        {
            if (id != inventoryCategory.InventoryCategoryId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    var inventoryCategoryDB = await _context.InventoryCategory.FindAsync(id);
                    var files = HttpContext.Request.Form.Files;
                    foreach (var Image in files)
                    {
                        if (Image != null && Image.Length > 0)
                        {
                            var file = Image;
                            var uploads = Path.Combine(_environment.WebRootPath, "Images\\Categorys");
                            if (file.Length > 0)
                            {
                                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                var Date = DateTime.Now.ToString("yyyyMMddHHmmss");
                                string ModFileName = Date + ".png";

                                if (ModFileName != null)
                                {
                                    //var _imageDB = _context.InventoryCategory.Where(x => x.InventoryCategoryId == id).Select(x => x.ImageUrl).FirstOrDefault();
                                    //Delete Previous image from folder
                                    if (System.IO.File.Exists(Path.Combine(_environment.WebRootPath, "Images\\Categorys") + inventoryCategoryDB.ImageUrl))
                                    {
                                        System.IO.File.Delete(Path.Combine(_environment.WebRootPath, "Images\\Categorys") + inventoryCategoryDB.ImageUrl);
                                    }
                                }
                                using (var fileStream = new FileStream(Path.Combine(uploads, ModFileName), FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                    inventoryCategoryDB.ImageUrl = ModFileName;
                                }
                            }
                        }
                    }
                    inventoryCategoryDB.CategoryName = inventoryCategory.CategoryName;
                    inventoryCategoryDB.UpdatedBy = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                    inventoryCategoryDB.UpdatedDate = DateTime.Now;
                    _context.Update(inventoryCategoryDB);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryCategoryExists(inventoryCategory.InventoryCategoryId))
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
            return View(inventoryCategory);
        }

        // GET: Categorie/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            var inventoryCategory = await _context.InventoryCategory.FindAsync(id);
            if (id != inventoryCategory.InventoryCategoryId)
            {
                return NotFound();
            }
            try
            {
                inventoryCategory.IsRemove = true;
                _context.Update(inventoryCategory);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryCategoryExists(inventoryCategory.InventoryCategoryId))
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

        private bool InventoryCategoryExists(long id)
        {
            return _context.InventoryCategory.Any(e => e.InventoryCategoryId == id);
        }

        private void Image_resize(string input_Image_Path, string output_Image_Path, int new_size)
        {
            const long quality = 50L;
            using (var image = new Bitmap(System.Drawing.Image.FromFile(input_Image_Path)))
            {
                var resized_Bitmap = new Bitmap(new_size, new_size);
                using (var graphics = Graphics.FromImage(resized_Bitmap))
                {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, new_size, new_size);
                    //using (var output = System.IO.File.Open(output_Image_Path, FileMode.Create))
                    //{
                    //    var qualityParamId = Encoder.Quality;
                    //    var encoderParameters = new EncoderParameters(1);
                    //    encoderParameters.Param[0] = new EncoderParameter(qualityParamId, quality);
                    //    var codec = ImageCodecInfo.GetImageDecoders().FirstOrDefault(c => c.FormatID == ImageFormat.Jpeg.Guid);
                    //    resized_Bitmap.Save(output, codec, encoderParameters);
                    //}
                }
            }
        }

    }
}
