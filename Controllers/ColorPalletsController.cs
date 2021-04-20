using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ColorApplication.Data;
using ColorApplication.Models;
using System.Text.Json;
using ColorApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Web;

namespace ColorApplication.Controllers
{
    public class ColorPalletsController : Controller
    {
        private readonly ColorPalletContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ColorPalletsController(ColorPalletContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ColorPallets
        public IActionResult Index(Guid? id)
        {
            ViewData["ID"] = id;
    
            if (User.Identity.IsAuthenticated)
            {
                Guid theUserID = Guid.Parse(_userManager.GetUserId(User));
                var query = $"SELECT * FROM ColorPallet WHERE UId = '{theUserID}';";
                var x = _context.ColorPallet
                    .Where(s => s.UId == theUserID)
                    .ToList();
                return View(x);
            }
            else
            {
                return View();
            }


            
        }

        public string ReturnId(Guid palletId)
        {
            return JsonSerializer.Serialize(palletId);
        }

        [HttpPost]
        public async Task<string> DeletePallet(string PalletId)
        {
            var colorPallet = await _context.ColorPallet.FindAsync(Guid.Parse(PalletId));
            _context.ColorPallet.Remove(colorPallet);
            await  _context.SaveChangesAsync();
            return PalletId;
   
        }


        // Save Pallet: Called from javacript function SavePallet via ajax.
        [HttpPost]
        public string SavePallets(string PalletId, string [] Colors, string PalletName, string PalletDescription, int PalletIsPublic, int PalletIsCopy)
        {
            string theUserID = _userManager.GetUserId(User);
            string colorStrings = String.Join('|', Colors);
            ColorPallet colorPallet = new();
            if (theUserID != null)
            {
                colorPallet.UId = Guid.Parse(theUserID);
            }

            colorPallet.Name = PalletName;
            colorPallet.Pallet = colorStrings;
            colorPallet.Description = PalletDescription;
            colorPallet.IsPublic = PalletIsPublic;
            colorPallet.IsCopy = PalletIsCopy;

            if (ModelState.IsValid)
            {
                // If null, it is a guest and not allowed to save pallets.
                // Guest must make an account for this feature.
                if (theUserID != null) {
                    if (PalletId == null)
                    {
                        colorPallet.Id = Guid.NewGuid();
                        _context.Add(colorPallet);
                    }
                    else
                    {
                        colorPallet.Id = Guid.Parse(PalletId);
                        _context.Update(colorPallet);
                    }
                   _context.SaveChangesAsync();
                }
            }
            return JsonSerializer.Serialize(colorPallet.Id);
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
