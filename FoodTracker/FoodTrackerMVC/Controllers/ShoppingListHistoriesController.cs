using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodTrackerMVC.Data;
using FoodTrackerMVC.Models;

namespace FoodTrackerMVC.Controllers
{
    public class ShoppingListHistoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShoppingListHistoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShoppingListHistories
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShoppingListHistory.ToListAsync());
        }

        // GET: ShoppingListHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListHistory = await _context.ShoppingListHistory
                .FirstOrDefaultAsync(m => m.IdList == id);
            if (shoppingListHistory == null)
            {
                return NotFound();
            }

            return View(shoppingListHistory);
        }

        // GET: ShoppingListHistories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShoppingListHistories/SaveListHistories
        public IActionResult SaveListHistories()
        {
            return View();
        }

        // POST: ShoppingListHistories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdList,IdUser")] ShoppingListHistory shoppingListHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shoppingListHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shoppingListHistory);
        }

        // GET: ShoppingListHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListHistory = await _context.ShoppingListHistory.FindAsync(id);
            if (shoppingListHistory == null)
            {
                return NotFound();
            }
            return View(shoppingListHistory);
        }

        // POST: ShoppingListHistories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdList,IdUser")] ShoppingListHistory shoppingListHistory)
        {
            if (id != shoppingListHistory.IdList)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shoppingListHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShoppingListHistoryExists(shoppingListHistory.IdList))
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
            return View(shoppingListHistory);
        }

        // GET: ShoppingListHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shoppingListHistory = await _context.ShoppingListHistory
                .FirstOrDefaultAsync(m => m.IdList == id);
            if (shoppingListHistory == null)
            {
                return NotFound();
            }

            return View(shoppingListHistory);
        }

        // POST: ShoppingListHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shoppingListHistory = await _context.ShoppingListHistory.FindAsync(id);
            _context.ShoppingListHistory.Remove(shoppingListHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShoppingListHistoryExists(int id)
        {
            return _context.ShoppingListHistory.Any(e => e.IdList == id);
        }
    }
}
