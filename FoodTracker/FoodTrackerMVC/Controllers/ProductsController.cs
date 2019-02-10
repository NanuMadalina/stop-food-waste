using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodTrackerMVC.Data;
using FoodTrackerMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Recombee.ApiClient;
using Recombee.ApiClient.ApiRequests;
using System.IO;
using Newtonsoft.Json;
using Recombee.ApiClient.Bindings;

namespace FoodTrackerMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Models.User> _userManager;

        public ProductsController(ApplicationDbContext context, UserManager<Models.User> userManager)
        {  
            _context = context;
            _userManager = userManager;
        }

        public void getData()
        {
            var client = new RecombeeClient("database-ip", "3FdY9t0vAwece7ih4wy4R5rt78y7zVppqv2xnOPcJIzzhcA5zUnrwYGnAZJMHKyu");

            //string[] allLines = System.IO.File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ML.csv"));
            //var parsed = from line in allLines
            //             let row = line.Split(',')
            //             select new
            //             {
            //                 fk_user = row[0],
            //                 product_name = row[1],
            //                 list_id_for_user = row[2],
            //             };
            //var purchases = parsed.Select(x => new AddPurchase(x.fk_user, x.product_name,null,cascadeCreate: true));

            //client.Send(new AddItemProperty("category_name","string"));
            //client.Send(new AddItemProperty("Kcal","int"));


            //using (StreamReader r = new StreamReader("jsonn.json"))
            //{
            //    string json = r.ReadToEnd();
            //    Dictionary<string, Dictionary<string, Object>> items =
            //            JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Object>>>(json);

            //    var requests = new List<SetItemValues>();

            //    foreach (KeyValuePair<string, Dictionary<string, Object>> entry in items)
            //    {
            //        var itemId = entry.Key;
            //        var values = entry.Value;
            //        requests.Add(new SetItemValues(itemId, values, cascadeCreate: true));
            //    }
            //    client.Send(new Batch(requests));
            //}

            var userId = _userManager.GetUserId(User);
            RecommendationResponse recommended = client.Send(new RecommendItemsToItem("Orange", userId, 5, returnProperties: true));
            var rec = recommended.Recomms.Select(x => x.Id).ToList();
            ViewBag.Recomandations = rec;
        }

        private static void ShowReco(RecommendationResponse recommended)
        {
            foreach (Recommendation rec in recommended.Recomms)
            {
                
                foreach (KeyValuePair<string, Object> entry in rec.Values)
                {
                    var propertyName = entry.Key;
                    var propertyValue = entry.Value;
                  
                }
            }
        }


        // GET: Productss

        public async Task<IActionResult> Index(int? categoryFilter)
        {
            getData();

            List<Product> result;
			List<Product_categories> categories = _context.Product_categories.ToList();
            //what if userId == null
            var userId =_userManager.GetUserId(User);
            List<Product_List> productList = _context.Product_List.Where(x => x.fk_user.Equals(userId)).ToList();
            ViewBag.Categories = categories;

            //ViewBag.Product_List = productList;

            List<Product> products = new List<Product>();
            foreach (var p in productList)
            {
                Product prod = _context.Product.Where(pr => pr.id_product == p.fk_product).FirstOrDefault();
                products.Add(prod);
            }
            //distinct! 
            //var currentListProducs = _context.Product_List.Where(x => x.fk_user.Equals(userId))
            //var productListForUser =

            var currentList = _context.Product_List.Where(x => x.fk_user.Equals(userId)).Select(p => p.list_id_for_user).DefaultIfEmpty(0).Max();
            var query = _context.Product
                   .Join(_context.Product_List,
                      p => p.id_product,
                      pl => pl.fk_product,
                      (p, pl) => new { Product = p, Product_List = pl })
                   .Where(x=> x.Product_List.fk_user == userId && x.Product_List.list_id_for_user == 0).ToList();
            ///

            var newProductListfromQuery = new List<Product>();
            foreach (var item in query)
            {
                newProductListfromQuery.Add(item.Product);
            }

            //
          
            //
            products = newProductListfromQuery.OrderBy(n => n.product_name).Distinct().ToList();
            ViewBag.Products = products;
            ViewBag.categoryFilter = categoryFilter;

			if (categoryFilter != null)
				result = await _context.Product.Where(x => x.id_category == categoryFilter).ToListAsync();
			else
				result = await _context.Product.ToListAsync();
			return View(result);
        } 

        public string VerifyKcal()
        {
            string message = "";
            var userId = _userManager.GetUserId(User);

            var query2 = _context.Product
          .Join(_context.Product_List,
              p => p.id_product,
              pl => pl.fk_product,
              (p, pl) => new { Product = p, Product_List = pl })
          .Where(x => x.Product_List.fk_user == userId && x.Product_List.list_id_for_user == 0).Select(x => x.Product.Kcal).ToList();
            var sumCal = 0;

            var maxF = 500;
            var maxM = 600;
            var userGender = _context.AspNetUsers.Where(x => x.Id == userId).Select(x => x.Gender).ToList();

            foreach (var item in query2)
            {
                sumCal += item;

            }

            if (userGender.Contains("f"))
            {
                if (sumCal >= maxF)
                {
                    message = "multe";
                    TempData["msg"] = "<script>alert('Ati ales cam multe produse');</script>";
                }
            }
           else  if (!userGender.Contains("f") && userGender.Equals("male"))
            {
                if (sumCal >= maxM)
                {
                    message = "multe";
                    TempData["msg"] = "<script>alert('Ati ales cam multe produse');</script>";
                    ViewBag.Alert = "multe";
                }
            }
            else
            {
                message = "putine";

            }
            
            return message;
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.id_product == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
			List<Product_categories> categories = _context.Product_categories.ToList();
			ViewBag.Categories = categories;
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_product,product_name,product_description,quantity,unit_of_measurement,id_category,valability,frequency_usage")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_product,product_name,product_description,quantity,unit_of_measurement,id_category,valability,frequency_usage,Kcal")] Product product)
        {
            if (id != product.id_product)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.id_product))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.id_product == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.id_product == id);
        }

        protected void btnTemp_Click(object sender, EventArgs e)
        {

        }
        public IActionResult incrementColumn()
        {
            var userId = _userManager.GetUserId(User);
            List<Product_List> productList = _context.Product_List.Where(x => x.fk_user.Equals(userId)).ToList();
            List<Product> products = _context.Product.ToList();
            var currentList = _context.Product_List.Where(x => x.fk_user.Equals(userId)).Select(p => p.list_id_for_user).DefaultIfEmpty(0).Max();
            var currentMmaxId = currentList + 1;
            //beforeMmaxId += 1;

            (from f in _context.Product_List
             where f.list_id_for_user.Equals(0)
                && f.fk_user == userId
             select f)
            .ToList()
            .ForEach(i => i.list_id_for_user = currentMmaxId);

          
            // Execute the query, and change the column values
            // you want to change.
            foreach (var pl in productList)
            {
                if(string.IsNullOrEmpty(pl.product_name))
                    pl.product_name = products.Where(x => x.id_product == pl.fk_product).Select(p => p.product_name).FirstOrDefault();
                // Insert any additional changes to column values.
            }


            var query = _context.Product
            .Join(_context.Product_List,
                p => p.id_product,
                pl => pl.fk_product,
                (p, pl) => new { Product = p, Product_List = pl })
            .Where(x => x.Product_List.fk_user == userId && x.Product_List.list_id_for_user == 0).Select(x=> x.Product.Kcal).ToList();
            var sumCal = 0;
           
            var maxF = 2000;
            var maxM = 2500;
            var userGender = _context.AspNetUsers.Where(x=> x.Id == userId).Select(x=> x.Gender).ToList();
           
            foreach (var item in query)
            {
                sumCal += item;
                
            }

            if (userGender.Contains("f"))
            {
                if (sumCal >= maxF)
                {
                    ViewBag.Alert = "Ati adaugat cam multe produse!";
                    TempData["msg"] = "<script>alert('Ati ales cam multe produse');</script>";


                }
            }
            else if (!userGender.Contains("f") && userGender.Equals("male"))
            {
                if (sumCal >= maxM)
                {
                    ViewBag.Alert = "Ati adaugat cam multe produse!";
                    TempData["msg"] = "<script>alert('Ati ales cam multe produse');</script>";


                }
            }
            else
            {
                //
                _context.SaveChanges();

            }
            // return beforeMmaxId;

            if (userGender.Contains("f"))
            {
                if (sumCal < maxF)
                    _context.SaveChanges();
            }
            if (!userGender.Contains("f"))
            {
                if (sumCal < maxM)
                    _context.SaveChanges();
            }

            productList = new List<Product_List>();

            return RedirectToAction(nameof(Index));

        }

       



        [Authorize]
        public IActionResult AddList(int id, int? categoryFilter)
        {
            //what if userId == null; with [Authorize] userId should never be null
            var userId = _userManager.GetUserId(User);

            //List<Product_List> productList = _context.Product_List.Where(x => x.fk_user.Equals(userId)).ToList();
            var newProductList = new Product_List();
            newProductList.fk_product = id;
            newProductList.fk_user = userId;

            //*much* less then ideal - might break for simultaneous users/connections/requests
            int maxId = _context.Product_List.Select(p => p.id_list).DefaultIfEmpty(0).Max();



            //newProductList.list_id_for_user = _context.Product_List.Select(p => p.list_id_for_user).DefaultIfEmpty(0).Max();
            newProductList.id_list = maxId + 1;
            newProductList.list_id_for_user = 0;
            _context.Product_List.Add(newProductList);
            _context.SaveChanges();

            if (categoryFilter != null)
                return RedirectToAction(nameof(Index), new { categoryFilter });

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public IActionResult DeleteList(int productId, int? categoryFilter)
        {
            //what if userId == null; with [Authorize] userId should never be null
            var userId = _userManager.GetUserId(User);

            //HF
            var product = _context.Product_List.Where(p => p.fk_product == productId && p.fk_user.Equals(userId) && p.list_id_for_user == 0).FirstOrDefault();
            if (product == null)
                return NotFound();
            _context.Product_List.Remove(product);

            _context.SaveChanges();

            if (categoryFilter != null)
                return RedirectToAction(nameof(Index), new { categoryFilter });

            return RedirectToAction(nameof(Index));
        }
    }
}
