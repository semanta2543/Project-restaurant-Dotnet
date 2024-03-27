using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class ContactUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactUsController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Index([Bind("Id,Name,ContactNumber,Message")] ContactUsModel contactUsModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactUsModel);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Your message has been sent successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(contactUsModel);
        }

        
        public async Task<IActionResult> ReviewMessages()
        {
            var reviewMessages = await _context.ContactUsModel.ToListAsync();
            return View(reviewMessages);
        }
    }
}
