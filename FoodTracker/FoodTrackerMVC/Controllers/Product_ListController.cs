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
using Microsoft.AspNetCore.Authorization;

namespace FoodTrackerMVC.Controllers
{
    public class Product_ListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public Product_ListController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Product_List
        [Authorize]
        public async Task<IActionResult> Index(int? IDSFilter)
        {
            List<Product_List> result;  
            var userId = _userManager.GetUserId(User);

            //randare :
            List<Product_List> lists = _context.Product_List.ToList();

            List<int> listsIDD = lists.OrderBy(x => x.list_id_for_user).Where(x => x.fk_user == userId).Select(x => x.list_id_for_user).Distinct().ToList();
            ViewBag.Listsdsd = listsIDD;
            ViewBag.Lists = lists;
            ViewBag.IDSFilter = IDSFilter;

            //produsele cu list_id = idsfilter

            List<Product_List> listaFinala = lists.Where(x => x.fk_user == userId).ToList();

            ViewBag.LISTAFINALA = listaFinala;
            if (IDSFilter != null)
                result = await _context.Product_List.Where(x => x.fk_user == userId && x.list_id_for_user == IDSFilter).ToListAsync();
            else
                result = await _context.Product_List.Where(x => x.fk_user == userId).ToListAsync();
            return View(result);
        }

        // GET: Product_List/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_List = await _context.Product_List
                .FirstOrDefaultAsync(m => m.id_list == id);
            if (product_List == null)
            {
                return NotFound();
            }

            return View(product_List);
        }

        // GET: Product_List/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product_List/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_list,list_id_for_user,fk_user,fk_product,product_name")] Product_List product_List)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product_List);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product_List);
        }

        // GET: Product_List/Edit/5
        public async Task<IActionResult> Edit(int? id)
        { 
            if (id == null)
            {
                return NotFound();
            }

            var product_List = await _context.Product_List.FindAsync(id);
            if (product_List == null)
            {
                return NotFound();
            }
            return View(product_List);
        }

        // POST: Product_List/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_list,list_id_for_user,fk_user,fk_product")] Product_List product_List)
        {
            if (id != product_List.id_list)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product_List);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Product_ListExists(product_List.id_list))
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
            return View(product_List);
        }

        // GET: Product_List/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product_List = await _context.Product_List
                .FirstOrDefaultAsync(m => m.id_list == id);
            if (product_List == null)
            {
                return NotFound();
            }

            return View(product_List);
        }

        // POST: Product_List/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product_List = await _context.Product_List.FindAsync(id);
            _context.Product_List.Remove(product_List);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Product_ListExists(int id)
        {
            return _context.Product_List.Any(e => e.id_list == id);
        }
    }
}
