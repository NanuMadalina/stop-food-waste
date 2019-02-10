using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodTrackerMVC.Data;
using FoodTrackerMVC.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodTrackerMVC.Controllers
{
    public class ListsToShowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ListsToShowsController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public void ListsJoin()
        {
            var userId = _userManager.GetUserId(User);
            
            var innerJoinQuery =
            from pl in _context.Product_List
            join p in _context.Product on pl.fk_product equals p.id_product
            join pc in _context.Product_categories on p.id_category equals pc.id_category
            where pl.fk_user == userId
            select new ListsToShow
            {
                IdList = pl.id_list,
                product_name = p.product_name,
                FkUser = pl.fk_user,
                FkProduct = pl.fk_product,
                id_list_for_user = pl.list_id_for_user
      
   };
        }
        // GET: ListsToShows
        public async Task<IActionResult> Index()
        {
           
            return View(await _context.ListsToShow.ToListAsync());
        }

        // GET: ListsToShows/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                
                return NotFound();
            }

            var listsToShow = await _context.ListsToShow
                .FirstOrDefaultAsync(m => m.IdList == id);
            if (listsToShow == null)
            {
                return NotFound();
            }

            return View(listsToShow);
        }

        // GET: ListsToShows/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ListsToShows/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdList,FkProduct,FkUser,product_name")] ListsToShow listsToShow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(listsToShow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(listsToShow);
        }

        // GET: ListsToShows/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listsToShow = await _context.ListsToShow.FindAsync(id);
            if (listsToShow == null)
            {
                return NotFound();
            }
            return View(listsToShow);
        }

        // POST: ListsToShows/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdList,FkProduct,FkUser,product_name")] ListsToShow listsToShow)
        {
            if (id != listsToShow.IdList)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(listsToShow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ListsToShowExists(listsToShow.IdList))
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
            return View(listsToShow);
        }

        // GET: ListsToShows/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var listsToShow = await _context.ListsToShow
                .FirstOrDefaultAsync(m => m.IdList == id);
            if (listsToShow == null)
            {
                return NotFound();
            }

            return View(listsToShow);
        }

        // POST: ListsToShows/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var listsToShow = await _context.ListsToShow.FindAsync(id);
            _context.ListsToShow.Remove(listsToShow);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ListsToShowExists(int id)
        {
            return _context.ListsToShow.Any(e => e.IdList == id);
        }
    }
}
