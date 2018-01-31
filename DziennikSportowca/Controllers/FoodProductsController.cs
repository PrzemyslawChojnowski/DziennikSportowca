using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DziennikSportowca.Data;
using DziennikSportowca.Models;
using DziennikSportowca.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace DziennikSportowca.Controllers
{
    [Authorize(Roles = "Admin")]
    public class FoodProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FoodProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: FoodProducts
        public async Task<IActionResult> Index()
        {
            FoodProductsViewModel model = new FoodProductsViewModel();

            model.Bakals = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Bakalie i nasiona").ToListAsync();
            model.CerealProducts = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Produkty zbożowe").ToListAsync();
            model.Drinks = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Napoje").ToListAsync();
            model.Fats = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Tłuszcze").ToListAsync();
            model.Fishes = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Ryby i dania rybne").ToListAsync();
            model.Fruits = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Owoce i ich przetwory").ToListAsync();
            model.MeatProducts = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Mięso, wędliny i dania mięsne").ToListAsync();
            model.MilkEggsProducts = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Nabiał i jaja").ToListAsync();
            model.PreparedProducts = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Produkty gotowe").ToListAsync();
            model.Sweets = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Słodycze i przekąski").ToListAsync();
            model.Vegetables = await _context.FoodProducts.Include(x => x.Type).Where(x => x.Type.Description == "Warzywa i ich przetwory").ToListAsync();

            return View(model);
        }

        // GET: FoodProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodProduct = await _context.FoodProducts
                .Include(x => x.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (foodProduct == null)
            {
                return NotFound();
            }

            return View(foodProduct);
        }

        // GET: FoodProducts/Create
        public IActionResult Create()
        {
            ViewData["FoodTypes"] = new SelectList(_context.FoodProductsTypes, "Id", "Description");
            return View();
        }

        // POST: FoodProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Energy,Protein,Fat,Carbohydrate,FoodProductTypeId")] CreateFoodProductViewModel foodProduct)
        {
            if (ModelState.IsValid)
            {
                FoodProductType type = await _context.FoodProductsTypes.FirstOrDefaultAsync(x => x.Id == foodProduct.FoodProductTypeId);

                FoodProduct product = new FoodProduct()
                {
                    Carbohydrate = foodProduct.Carbohydrate,
                    Description = foodProduct.Description,
                    Energy = foodProduct.Energy,
                    Fat = foodProduct.Fat,
                    Protein = foodProduct.Protein,
                    Measurement = Measurement.Weight,
                    Type = type
                };

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodTypes"] = new SelectList(_context.FoodProductsTypes, "Id", "Description");
            return View(foodProduct);
        }

        // GET: FoodProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodProduct = await _context.FoodProducts.Include(x => x.Type).SingleOrDefaultAsync(m => m.Id == id);
            if (foodProduct == null)
            {
                return NotFound();
            }

            EditFoodProductViewModel model = new EditFoodProductViewModel()
            {
                Carbohydrate = foodProduct.Carbohydrate,
                FoodProductTypeId = foodProduct.Type.Id,
                Id = foodProduct.Id,
                Description = foodProduct.Description,
                Energy = foodProduct.Energy,
                Fat = foodProduct.Fat,
                Protein = foodProduct.Protein
            };

            ViewData["FoodTypes"] = new SelectList(_context.FoodProductsTypes, "Id", "Description");

            return View(model);
        }

        // POST: FoodProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Energy,Protein,Fat,Carbohydrate,FoodProductTypeId")] EditFoodProductViewModel foodProduct)
        {
            if (id != foodProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    FoodProductType type = await _context.FoodProductsTypes.FirstOrDefaultAsync(x => x.Id == foodProduct.FoodProductTypeId);

                    FoodProduct product = new FoodProduct()
                    {
                        Protein = foodProduct.Protein,
                        Fat = foodProduct.Fat,
                        Energy = foodProduct.Energy,
                        Description = foodProduct.Description,
                        Id = foodProduct.Id,
                        Carbohydrate = foodProduct.Carbohydrate,
                        Measurement = Measurement.Weight,
                        Type = type
                    };

                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodProductExists(foodProduct.Id))
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
            return View(foodProduct);
        }

        // GET: FoodProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodProduct = await _context.FoodProducts
                .Include(x => x.Type)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (foodProduct == null)
            {
                return NotFound();
            }

            return View(foodProduct);
        }

        // POST: FoodProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodProduct = await _context.FoodProducts.SingleOrDefaultAsync(m => m.Id == id);
            _context.FoodProducts.Remove(foodProduct);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodProductExists(int id)
        {
            return _context.FoodProducts.Any(e => e.Id == id);
        }
    }
}
