using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SonghitsApp.Data;
using SonghitsApp.Models;

namespace SonghitsApp.Controllers
{
    public class songhitController : Controller
    {
        private readonly ApplicationDbContext _context;

        public songhitController(ApplicationDbContext context)
        {
            _context = context;
        }

        //// GET: songhit
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Songhit.ToListAsync());
        //}

        // GET: Songlists
        public async Task<IActionResult> Index(string searchBy, string search)
        {
            //return View(await _context.Songlists.ToListAsync());
            if (searchBy == "song")
            {
                return View(await _context.Songhit.Where(x => x.Song.StartsWith(search) || search == null).ToListAsync());
            }
            else
            {
                return View(await _context.Songhit.Where(x => x.artist.StartsWith(search) || search == null).ToListAsync());
            }
        }



        // GET: songhit/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songhit = await _context.Songhit
                .FirstOrDefaultAsync(m => m.id == id);
            if (songhit == null)
            {
                return NotFound();
            }

            return View(songhit);
        }

        // GET: songhit/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: songhit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,no,Song,artist,description,remarks")] songhit songhit)
        {
            if (ModelState.IsValid)
            {
                _context.Add(songhit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(songhit);
        }

        // GET: songhit/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songhit = await _context.Songhit.FindAsync(id);
            if (songhit == null)
            {
                return NotFound();
            }
            return View(songhit);
        }

        // POST: songhit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,no,Song,artist,description,remarks")] songhit songhit)
        {
            if (id != songhit.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(songhit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!songhitExists(songhit.id))
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
            return View(songhit);
        }

        // GET: songhit/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var songhit = await _context.Songhit
                .FirstOrDefaultAsync(m => m.id == id);
            if (songhit == null)
            {
                return NotFound();
            }

            return View(songhit);
        }

        // POST: songhit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var songhit = await _context.Songhit.FindAsync(id);
            if (songhit != null)
            {
                _context.Songhit.Remove(songhit);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool songhitExists(string id)
        {
            return _context.Songhit.Any(e => e.id == id);
        }
    }
}
