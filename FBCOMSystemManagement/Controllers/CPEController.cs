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
    public class CPEController : Controller
    {
        private readonly DBContextFBCOM _context;

        public CPEController(DBContextFBCOM context)
        {
            _context = context;
        }

        // GET: CPE
        public async Task<IActionResult> Index()
        {
              return View(await _context.CPES.Where(m=>m.Installed=="NON").ToListAsync());
        }

        // GET: CPE/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CPES == null)
            {
                return NotFound();
            }

            var cPE = await _context.CPES
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cPE == null)
            {
                return NotFound();
            }

            return View(cPE);
        }

        // GET: CPE/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CPE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,SNAntenne,SNRouteur,IMEI,Description,DateAjout,Installed")] CPE cPE)
        {
            if (ModelState.IsValid)
            {
                cPE.DateAjout= DateTime.Now;    
                cPE.Installed= "NON";    
                _context.Add(cPE);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cPE);
        }

        // GET: CPE/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CPES == null)
            {
                return NotFound();
            }

            var cPE = await _context.CPES.FindAsync(id);
            if (cPE == null)
            {
                return NotFound();
            }
            return View(cPE);
        }

        // POST: CPE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,SNAntenne,SNRouteur,IMEI,Description,DateAjout,Installed")] CPE cPE)
        {
            if (id != cPE.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    cPE.Installed = "NON";
                    _context.Update(cPE);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CPEExists(cPE.ID))
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
            return View(cPE);
        }

        // GET: CPE/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CPES == null)
            {
                return NotFound();
            }

            var cPE = await _context.CPES
                .FirstOrDefaultAsync(m => m.ID == id);
            if (cPE == null)
            {
                return NotFound();
            }

            return View(cPE);
        }

        // POST: CPE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CPES == null)
            {
                return Problem("Entity set 'DBContextFBCOM.CPES'  is null.");
            }
            var cPE = await _context.CPES.FindAsync(id);
            if (cPE != null)
            {
                _context.CPES.Remove(cPE);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CPEExists(int id)
        {
          return _context.CPES.Any(e => e.ID == id);
        }
    }
}
