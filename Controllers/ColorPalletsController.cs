using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ColorApplication.Data;
using ColorApplication.Models;

namespace ColorApplication.Controllers
{
    public class ColorPalletsController : Controller
    {
        private readonly ColorPalletContext _context;

        public ColorPalletsController(ColorPalletContext context)
        {
            _context = context;
        }

        // GET: ColorPallets
        public async Task<IActionResult> Index()
        {
            return View(await _context.ColorPallet.ToListAsync());
        }

        // GET: ColorPallets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorPallet = await _context.ColorPallet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorPallet == null)
            {
                return NotFound();
            }

            return View(colorPallet);
        }

        // GET: ColorPallets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ColorPallets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UId,Name,Description,Pallet,IsPublic,IsCopy")] ColorPallet colorPallet)
        {
            if (ModelState.IsValid)
            {
                colorPallet.Id = Guid.NewGuid();
                _context.Add(colorPallet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(colorPallet);
        }

        // GET: ColorPallets/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorPallet = await _context.ColorPallet.FindAsync(id);
            if (colorPallet == null)
            {
                return NotFound();
            }
            return View(colorPallet);
        }

        // POST: ColorPallets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UId,Name,Description,Pallet,IsPublic,IsCopy")] ColorPallet colorPallet)
        {
            if (id != colorPallet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colorPallet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorPalletExists(colorPallet.Id))
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
            return View(colorPallet);
        }

        // GET: ColorPallets/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var colorPallet = await _context.ColorPallet
                .FirstOrDefaultAsync(m => m.Id == id);
            if (colorPallet == null)
            {
                return NotFound();
            }

            return View(colorPallet);
        }

        // POST: ColorPallets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var colorPallet = await _context.ColorPallet.FindAsync(id);
            _context.ColorPallet.Remove(colorPallet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorPalletExists(Guid id)
        {
            return _context.ColorPallet.Any(e => e.Id == id);
        }
    }
}
