using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using DziennikSportowca.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DziennikSportowca.Data;
using DziennikSportowca.Models;

namespace DziennikSportowca.Controllers
{
    public class DishesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public DishesController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: Dishes
        public async Task<IActionResult> Index()
        {
            var userId = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));
            var dishes = await _context.Dishes.Include(x => x.FoodProducts).ThenInclude(x => x.FoodProduct).Where(x => x.UserId == userId).ToListAsync();

            if (dishes == null)
            {
                return NotFound();
            }

            List<DishDetailsViewModel> model = new List<DishDetailsViewModel>();

            if (dishes.Any())
            {
                foreach (var dish in dishes)
                {
                    double totalProteins = 0;
                    double totalFat = 0;
                    double totalCarbs = 0;
                    double totalEnergy = 0;
                    double totalWeight = 0;

                    foreach (var product in dish.FoodProducts)
                    {
                        totalCarbs += (product.FoodProduct.Carbohydrate * product.FoodProductWeight / 100);
                        totalFat += (product.FoodProduct.Fat * product.FoodProductWeight / 100);
                        totalProteins += (product.FoodProduct.Protein * product.FoodProductWeight / 100);
                        totalEnergy += (product.FoodProduct.Energy * product.FoodProductWeight / 100);
                        totalWeight += product.FoodProductWeight;
                    }
                    DishDetailsViewModel dishVMElement = new DishDetailsViewModel()
                    {
                        Dish = dish,
                        TotalCarbs = totalCarbs,
                        TotalEnergy = totalEnergy,
                        TotalFat = totalFat,
                        TotalProteins = totalProteins,
                        TotalWeight = totalWeight
                    };
                    model.Add(dishVMElement);
                }
            }
            return View(model);
        }

        // GET: Dishes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(x => x.FoodProducts)
                    .ThenInclude(x => x.FoodProduct)
                        .ThenInclude(x => x.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            double totalProteins = 0;
            double totalFat = 0;
            double totalCarbs = 0;
            double totalEnergy = 0;
            double totalWeight = 0;

            foreach(var product in dish.FoodProducts)
            {
                totalCarbs += (product.FoodProduct.Carbohydrate * product.FoodProductWeight / 100);
                totalFat += (product.FoodProduct.Fat * product.FoodProductWeight / 100);
                totalProteins += (product.FoodProduct.Protein * product.FoodProductWeight / 100);
                totalEnergy += (product.FoodProduct.Energy * product.FoodProductWeight / 100);
                totalWeight += product.FoodProductWeight;
            }
            DishDetailsViewModel model = new DishDetailsViewModel()
            {
                Dish = dish,
                TotalCarbs = totalCarbs,
                TotalEnergy = totalEnergy,
                TotalFat = totalFat,
                TotalProteins = totalProteins,
                TotalWeight = totalWeight
            };

            return View(model);
        }

        // GET: Dishes/Create
        public IActionResult Create()
        {
            DishViewModel model = new DishViewModel();
            model.FoodProductType = new SelectList(_context.FoodProductsTypes.Select(x => x.Description));
            
            return View(model);
        }

        // POST: Dishes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string jsonData, string dishName)
        {
            if (String.IsNullOrEmpty(jsonData) || String.IsNullOrEmpty(dishName))
                return NotFound();

            ApplicationUser user = await _manager.GetUserAsync(User);
            
            var dishProducts = JsonConvert.DeserializeAnonymousType(jsonData,
                new[] {
                    new { foodType = "",
                        name = "",
                        weight = (double)0,
                        protein = (double)0,
                        carbohydrates = (double)0,
                        fat = (double)0,
                        kcal = (double)0 
                    }
                }.ToList());

            Dish dish = new Dish()
            {
                Description = dishName,
                UserId = user.Id,
                User = user
            };

            _context.Dishes.Add(dish);
            await _context.SaveChangesAsync();

            foreach(var element in dishProducts)
            {
                FoodProduct product = await _context.FoodProducts.FirstOrDefaultAsync(x => x.Description == element.name && 
                                                                                        x.Type.Description == element.foodType);

                DishFoodProduct dishProduct = new DishFoodProduct()
                {
                    FoodProduct = product,
                    FoodProductId = product.Id,
                    Dish = dish,
                    DishId = dish.Id,
                    FoodProductWeight = element.weight
                };

                _context.DishFoodProducts.Add(dishProduct);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // GET: Dishes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }
            return View(dish);
        }

        // POST: Dishes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description")] Dish dish)
        {
            if (id != dish.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dish);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DishExists(dish.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(dish);
        }

        // GET: Dishes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dish = await _context.Dishes
                .Include(x => x.FoodProducts)
                    .ThenInclude(x => x.FoodProduct)
                        .ThenInclude(x => x.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (dish == null)
            {
                return NotFound();
            }

            double totalProteins = 0;
            double totalFat = 0;
            double totalCarbs = 0;
            double totalEnergy = 0;
            double totalWeight = 0;

            foreach (var product in dish.FoodProducts)
            {
                totalCarbs += (product.FoodProduct.Carbohydrate * product.FoodProductWeight / 100);
                totalFat += (product.FoodProduct.Fat * product.FoodProductWeight / 100);
                totalProteins += (product.FoodProduct.Protein * product.FoodProductWeight / 100);
                totalEnergy += (product.FoodProduct.Energy * product.FoodProductWeight / 100);
                totalWeight += product.FoodProductWeight;
            }
            DishDetailsViewModel model = new DishDetailsViewModel()
            {
                Dish = dish,
                TotalCarbs = totalCarbs,
                TotalEnergy = totalEnergy,
                TotalFat = totalFat,
                TotalProteins = totalProteins,
                TotalWeight = totalWeight
            };

            return View(model);
        }

        // POST: Dishes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dish = await _context.Dishes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Dishes.Remove(dish);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool DishExists(int id)
        {
            return _context.Dishes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> NutritionFacts (string type)
        {
            List<FoodProduct> nutritionFacts = await _context.FoodProducts.Include(x => x.Type).OrderBy(x => x.Type).ToListAsync();
            SelectList types = new SelectList(await _context.FoodProductsTypes.Select(x => x.Description).ToListAsync());

            if (!String.IsNullOrEmpty(type))
                nutritionFacts = nutritionFacts.Where(x => x.Type.Description == type).ToList();

            NutritionFactsViewModel model = new NutritionFactsViewModel()
            {
                FoodProducts = nutritionFacts,
                FoodProductTypes = types
            };

            return View(model);
        }

        public JsonResult GetProducts (string foodType)
        {
            List<string> foodProducts = _context.FoodProducts.Where(x => x.Type.Description == foodType).Select(x => x.Description).ToList();
            return Json(foodProducts);
        }

        public JsonResult GetNutritionFacts (string foodProduct)
        {
            var nutritionFacts = _context.FoodProducts.FirstOrDefault(x => x.Description == foodProduct);
            return Json(nutritionFacts);
        }
    }
}
