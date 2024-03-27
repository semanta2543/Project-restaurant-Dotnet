using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        public ProductsController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
     

        public async Task<IActionResult> Index()
        {
            var produkte = await _context.Products.Include(p => p.Category).ToListAsync();

            return View(produkte);
        }
       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
       
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_context.Category, "CategoryId", "CategoryName");
            return View();
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Discription,CategoryId,Image,ImageFile")] Products product,
           IFormFile ImageFile)
        {


           // if (ModelState.IsValid)
            {

                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images/", ImageFile.FileName);
                var directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                    stream.Close();
                }

                product.Image = ImageFile.FileName;
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }


            ViewBag.CategoryId = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
     
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            ViewBag.CategoryId = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }
       
            

            [HttpPost]
            [ValidateAntiForgeryToken]
    
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Discription,CategoryId,Image,ImageFile")] Products product,
                IFormFile ImageFile)
            {
                if (id != product.Id)
                {
                    return NotFound();
                }

               // if (ModelState.IsValid)
                {
                    try
                    {
                        if (ImageFile != null)
                        {
                            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images/", ImageFile.FileName);
                            var directory = Path.GetDirectoryName(path);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                            }

                            using (FileStream stream = new FileStream(path, FileMode.Create))
                            {
                                await ImageFile.CopyToAsync(stream);
                                stream.Close();
                            }

                            product.Image = ImageFile.FileName;
                        }

                        _context.Update(product);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProductExists(product.Id))
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

                ViewBag.CategoryId = new SelectList(_context.Category, "CategoryId", "CategoryName", product.CategoryId);
                return View(product);
            }
     

        public async Task<IActionResult> Delete(int? id)
            {
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Products
                    .Include(p => p.Category)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }

            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
            {
                var product = await _context.Products.FindAsync(id);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        private bool ProductExists(int id)
            {
                return _context.Products.Any(e => e.Id == id);
            }
        // ProductsController.cs
        public IActionResult Menu(string searchTerm, int? categoryFilter)
        {
            var categories = _context.Category
                .Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                })
                .ToList();

            ViewBag.Categories = categories;

            var menuItems = _context.Products
                .Where(p =>
                    (string.IsNullOrEmpty(searchTerm)  || p.Discription.Contains(searchTerm)) &&
                    (!categoryFilter.HasValue || p.CategoryId == categoryFilter)
                )
                .Select(p => new MenuViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Discription,
                    Price = p.Price,
                    Image = p.Image
                })
                .ToList();

            if (menuItems.Count == 0)
            {
                ViewBag.NoItemsMessage = "No products found.";
            }

            ViewBag.CategoryFilter = categoryFilter ?? 0;

            return View(menuItems);
        }







    }



}
