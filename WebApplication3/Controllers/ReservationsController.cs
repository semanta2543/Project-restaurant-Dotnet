using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WebApplication3.Data;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([Bind("Id,Name,ContactNumber,DateTime,Guests")] ReservationModel reservationModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationModel);
                await _context.SaveChangesAsync();

                // Ktheje ne ReservationResult duke bartur objektin e rezervimit
                return RedirectToAction("ReservationResult", reservationModel);
            }
            return View(reservationModel);
        }

        public IActionResult ReservationResult(ReservationModel reservationModel)
        {
            // Shfaq view-in e rezultatit të rezervimit
            return View(reservationModel);
        }

    }
}
