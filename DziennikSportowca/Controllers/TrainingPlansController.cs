using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DziennikSportowca.Data;
using Microsoft.AspNetCore.Identity;
using DziennikSportowca.Models.ViewModels;
using Newtonsoft.Json.Linq;

namespace DziennikSportowca.Models
{
    public class TrainingPlansController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public TrainingPlansController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: TrainingPlans
        public async Task<IActionResult> Index()
        {
            var loggedUser = _manager.GetUserId(User);
            var applicationDbContext = _context.TrainingPlans.Where(t => t.UserId == loggedUser).Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TrainingPlans/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPlan = await _context.TrainingPlans
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (trainingPlan == null)
            {
                return NotFound();
            }

            return View(trainingPlan);
        }

        // GET: TrainingPlans/Create
        public async Task<IActionResult> Create()
        {
            IQueryable<string> muscleParts = _context.MuscleParts.Select(x => x.Description);
            IQueryable<Exercise> exercises = from e in _context.Exercises
                                           select e;

            TrainingPlanViewModel trainingPlanViewModel = new TrainingPlanViewModel();
            trainingPlanViewModel.MuscleParts = new SelectList(await muscleParts.Distinct().ToListAsync());
            trainingPlanViewModel.Exercises = await exercises.ToListAsync();
            return View(trainingPlanViewModel);
        }

        // POST: TrainingPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Description")] TrainingPlan trainingPlan)
        {
            if (ModelState.IsValid)
            {
                var loggedUser = _manager.GetUserId(User);
                trainingPlan.UserId = loggedUser;
                _context.Add(trainingPlan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(trainingPlan);
        }

        // GET: TrainingPlans/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(m => m.Id == id);
            if (trainingPlan == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", trainingPlan.UserId);
            return View(trainingPlan);
        }

        // POST: TrainingPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,UserId")] TrainingPlan trainingPlan)
        {
            if (id != trainingPlan.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trainingPlan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrainingPlanExists(trainingPlan.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", trainingPlan.UserId);
            return View(trainingPlan);
        }

        // GET: TrainingPlans/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPlan = await _context.TrainingPlans
                .Include(t => t.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (trainingPlan == null)
            {
                return NotFound();
            }

            return View(trainingPlan);
        }

        // POST: TrainingPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(m => m.Id == id);
            _context.TrainingPlans.Remove(trainingPlan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool TrainingPlanExists(int id)
        {
            return _context.TrainingPlans.Any(e => e.Id == id);
        }

        // GET: TrainingPlans/AddTrainingExercises/5
        public async Task<IActionResult> AddTrainingExercises(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trainingPlan = await _context.TrainingPlans.SingleOrDefaultAsync(m => m.Id == id);
            if (trainingPlan == null)
            {
                return NotFound();
            }
            return View(trainingPlan);
        }

        public JsonResult getExercises(string trainingPartName)
        {
            //string trainingPartName = "";
            List<string> exercises = _context.MusclePartExercises
                .Where(x => x.MusclePart.Description == trainingPartName)
                .Select(x => x.Exercise.Name).ToList();
            return Json(exercises);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddTrainingExercises(int id, [Bind("Id,Description,UserId")] TrainingPlan trainingPlan)
        //{
        //    if (id != trainingPlan.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(trainingPlan);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!TrainingPlanExists(trainingPlan.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", trainingPlan.UserId);
        //    return View(trainingPlan);
        //}
    }
}
