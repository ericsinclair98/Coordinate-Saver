using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Controllers
{

    public class CoordinatesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CoordinatesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Coordinates && current user email
        public async Task<IActionResult> Index()
        {
            return _context.Coordinate != null ?
                        View(await _context.Coordinate.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Coordinate'  is null.");
        }
        // GET: Coordinates/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: Coordinates/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.Coordinate.Where(j => j.Description.Contains(SearchPhrase)).ToListAsync());
        }

        // GET: Coordinates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Coordinate == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // GET: Coordinates/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Coordinates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description,XValue,YValue,ZValue,Owner")] Coordinate coordinate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coordinate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coordinate);
        }

        // GET: Coordinates/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Coordinate == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinate.FindAsync(id);
            if (coordinate == null)
            {
                return NotFound();
            }
            return View(coordinate);
        }

        // POST: Coordinates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,XValue,YValue,ZValue,Owner")] Coordinate coordinate)
        {
            if (id != coordinate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coordinate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CoordinateExists(coordinate.Id))
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
            return View(coordinate);
        }

        // GET: Coordinates/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Coordinate == null)
            {
                return NotFound();
            }

            var coordinate = await _context.Coordinate
                .FirstOrDefaultAsync(m => m.Id == id);
            if (coordinate == null)
            {
                return NotFound();
            }

            return View(coordinate);
        }

        // POST: Coordinates/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Coordinate == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Coordinate'  is null.");
            }
            var coordinate = await _context.Coordinate.FindAsync(id);
            if (coordinate != null)
            {
                _context.Coordinate.Remove(coordinate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CoordinateExists(int id)
        {
            return (_context.Coordinate?.Any(e => e.Id == id)).GetValueOrDefault();
        }

    }
}
