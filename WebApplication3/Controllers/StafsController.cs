using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using restSakei.Models;

namespace WebApplication3.Controllers
{
    public class StafsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StafsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Stafs
        public async Task<IActionResult> Index()
        {
              return _context.Staf != null ? 
                          View(await _context.Staf.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Staf'  is null.");
        }

        // GET: Stafs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Staf == null)
            {
                return NotFound();
            }

            var staf = await _context.Staf
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staf == null)
            {
                return NotFound();
            }

            return View(staf);
        }

        // GET: Stafs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Stafs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ContactNumber,Salary,HiringDate,Position")] Staf staf)
        {
            if (ModelState.IsValid)
            {
                _context.Add(staf);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(staf);
        }

        // GET: Stafs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Staf == null)
            {
                return NotFound();
            }

            var staf = await _context.Staf.FindAsync(id);
            if (staf == null)
            {
                return NotFound();
            }
            return View(staf);
        }

        // POST: Stafs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ContactNumber,Salary,HiringDate,Position")] Staf staf)
        {
            if (id != staf.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(staf);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StafExists(staf.Id))
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
            return View(staf);
        }

        // GET: Stafs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Staf == null)
            {
                return NotFound();
            }

            var staf = await _context.Staf
                .FirstOrDefaultAsync(m => m.Id == id);
            if (staf == null)
            {
                return NotFound();
            }

            return View(staf);
        }

        // POST: Stafs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Staf == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Staf'  is null.");
            }
            var staf = await _context.Staf.FindAsync(id);
            if (staf != null)
            {
                _context.Staf.Remove(staf);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StafExists(int id)
        {
          return (_context.Staf?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
