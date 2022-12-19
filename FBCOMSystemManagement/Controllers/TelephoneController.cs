using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FBCOMSystemManagement.DATA;
using FBCOMSystemManagement.Models;

namespace FBCOMSystemManagement.Controllers
{
    public class TelephoneController : Controller
    {
        private readonly DBContextFBCOM _context;

        public TelephoneController(DBContextFBCOM context)
        {
            _context = context;
        }

        // GET: Telephone
        public async Task<IActionResult> Index()
        {
              return View(await _context.Telephones.Where(m => m.Installed == "NON").ToListAsync());
        }

        // GET: Telephone/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Telephones == null)
            {
                return NotFound();
            }

            var telephone = await _context.Telephones
                .FirstOrDefaultAsync(m => m.ID == id);
            if (telephone == null)
            {
                return NotFound();
            }

            return View(telephone);
        }

        // GET: Telephone/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Telephone/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SN,Installed,Description,DateAjout")] Telephone telephone)
        {
            if (ModelState.IsValid)
            {
                telephone.DateAjout = DateTime.Now;
                telephone.Installed = "NON";
                _context.Add(telephone);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(telephone);
        }

        // GET: Telephone/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Telephones == null)
            {
                return NotFound();
            }

            var telephone = await _context.Telephones.FindAsync(id);
            if (telephone == null)
            {
                return NotFound();
            }
            return View(telephone);
        }

        // POST: Telephone/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SN,Installed,Description,DateAjout")] Telephone telephone)
        {
            if (id != telephone.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    telephone.Installed = "NON";
                    _context.Update(telephone);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TelephoneExists(telephone.ID))
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
            return View(telephone);
        }

        // GET: Telephone/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Telephones == null)
            {
                return NotFound();
            }

            var telephone = await _context.Telephones
                .FirstOrDefaultAsync(m => m.ID == id);
            if (telephone == null)
            {
                return NotFound();
            }

            return View(telephone);
        }

        // POST: Telephone/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Telephones == null)
            {
                return Problem("Entity set 'DBContextFBCOM.Telephones'  is null.");
            }
            var telephone = await _context.Telephones.FindAsync(id);
            if (telephone != null)
            {
                _context.Telephones.Remove(telephone);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TelephoneExists(int id)
        {
          return _context.Telephones.Any(e => e.ID == id);
        }
    }
}
