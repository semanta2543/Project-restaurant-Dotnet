using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApplication3.Data;
using System.Collections.Generic;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Name,Email,Rating,Message")] ReviewModel reviewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reviewModel);
                await _context.SaveChangesAsync();
                

                return RedirectToAction("ReviewResult", reviewModel);
            }
            return View(reviewModel);
        }

        public IActionResult ReviewResult()
        {
            var reviews = _context.ReviewModel.OrderByDescending(r => r.Id).Take(5).ToList();
            return View(reviews);
        }


    }
}
