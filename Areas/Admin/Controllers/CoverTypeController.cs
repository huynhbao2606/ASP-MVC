using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_MVC.Data;
using ASP_MVC.Models;

namespace ASP_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoverTypeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/CoverType
        public async Task<IActionResult> Index()
        {
              return _context.CoverTypes != null ? 
                          View(await _context.CoverTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.CoverTypes'  is null.");
        }

        // GET: Admin/CoverType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CoverTypes == null)
            {
                return NotFound();
            }

            var coverType = await _context.CoverTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // GET: Admin/CoverType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/CoverType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coverType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        // GET: Admin/CoverType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CoverTypes == null)
            {
                return NotFound();
            }

            var coverType = await _context.CoverTypes.FindAsync(id);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        // POST: Admin/CoverType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CoverType coverType)
        {
            if (id != coverType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coverType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoverTypeExists(coverType.Id))
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
            return View(coverType);
        }

        // GET: Admin/CoverType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CoverTypes == null)
            {
                return NotFound();
            }

            var coverType = await _context.CoverTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }

        // POST: Admin/CoverType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CoverTypes == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CoverTypes'  is null.");
            }
            var coverType = await _context.CoverTypes.FindAsync(id);
            if (coverType != null)
            {
                _context.CoverTypes.Remove(coverType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoverTypeExists(int id)
        {
          return (_context.CoverTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
