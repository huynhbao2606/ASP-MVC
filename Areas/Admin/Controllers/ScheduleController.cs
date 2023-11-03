using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP_MVC.Data;
using ASP_MVC.Models;

namespace ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Schedule
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VaccinationSchedules.Include(v => v.Vaccine);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Schedule/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VaccinationSchedules == null)
            {
                return NotFound();
            }

            var vaccinationSchedule = await _context.VaccinationSchedules
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccinationSchedule == null)
            {
                return NotFound();
            }

            return View(vaccinationSchedule);
        }

        // GET: Admin/Schedule/Create
        public IActionResult Create()
        {
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "CountryOfManufacture");
            return View();
        }

        // POST: Admin/Schedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,VaccinationDates,VaccineId,CreatedAt")] VaccinationSchedule vaccinationSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vaccinationSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "CountryOfManufacture", vaccinationSchedule.VaccineId);
            return View(vaccinationSchedule);
        }

        // GET: Admin/Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VaccinationSchedules == null)
            {
                return NotFound();
            }

            var vaccinationSchedule = await _context.VaccinationSchedules.FindAsync(id);
            if (vaccinationSchedule == null)
            {
                return NotFound();
            }
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "CountryOfManufacture", vaccinationSchedule.VaccineId);
            return View(vaccinationSchedule);
        }

        // POST: Admin/Schedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,VaccinationDates,VaccineId,CreatedAt")] VaccinationSchedule vaccinationSchedule)
        {
            if (id != vaccinationSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vaccinationSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationScheduleExists(vaccinationSchedule.Id))
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
            ViewData["VaccineId"] = new SelectList(_context.Vaccines, "Id", "CountryOfManufacture", vaccinationSchedule.VaccineId);
            return View(vaccinationSchedule);
        }

        // GET: Admin/Schedule/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VaccinationSchedules == null)
            {
                return NotFound();
            }

            var vaccinationSchedule = await _context.VaccinationSchedules
                .Include(v => v.Vaccine)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccinationSchedule == null)
            {
                return NotFound();
            }

            return View(vaccinationSchedule);
        }

        // POST: Admin/Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VaccinationSchedules == null)
            {
                return Problem("Entity set 'ApplicationDbContext.VaccinationSchedules'  is null.");
            }
            var vaccinationSchedule = await _context.VaccinationSchedules.FindAsync(id);
            if (vaccinationSchedule != null)
            {
                _context.VaccinationSchedules.Remove(vaccinationSchedule);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationScheduleExists(int id)
        {
          return (_context.VaccinationSchedules?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
