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
using Newtonsoft.Json;

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
            return View();
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
                var id = trainingPlan.Id;
                return RedirectToAction("AddTrainingExercises", new { id = id });
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
            var trainingPlanExercises = _context.TrainingPlanExercises.Where(x => x.TrainingPlanId == trainingPlan.Id);
            _context.TrainingPlanExercises.RemoveRange(trainingPlanExercises);
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
            IQueryable<string> muscleParts = _context.MuscleParts.Select(x => x.Description);
            TrainingPlan plan = await _context.TrainingPlans.FirstOrDefaultAsync(x => x.Id == id);

            List<TrainingPlanExercise> exercises = await _context.TrainingPlanExercises.
                                                    Where(x => x.TrainingPlanId == plan.Id).
                                                    Include(x => x.Exercise).
                                                    Include(x => x.Exercise.MuscleParts).
                                                    ToListAsync();

            foreach(var exercise in exercises)
            {
                exercise.Exercise.MuscleParts = await _context.MusclePartExercises.Where(x => x.ExerciseId == exercise.ExerciseId).Include(x => x.MusclePart).ToListAsync();
            }

            TrainingPlanViewModel trainingPlanViewModel = new TrainingPlanViewModel();
            trainingPlanViewModel.MuscleParts = new SelectList(await muscleParts.Distinct().ToListAsync());
            plan.Exercises = exercises;
            trainingPlanViewModel.TrainingPlan = plan;
            
            return View(trainingPlanViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddTrainingExercises(string jsonData, int id)
        {
            TrainingPlanExercises result = JsonConvert.DeserializeObject<TrainingPlanExercises>(jsonData);
            TrainingPlan plan = await _context.TrainingPlans.SingleOrDefaultAsync(x => x.Id == id);
            List<TrainingPlanExercise> tpExercises = await _context.TrainingPlanExercises.
                                                Where(x => x.TrainingPlan.Id == plan.Id).ToListAsync();

            for (int i = 0; i < result.rowNo.Count; i++)
            {                
                Exercise exercise = await _context.Exercises.SingleOrDefaultAsync(x => x.Name == result.exercise[i] && 
                                                x.MuscleParts.Any(z => z.MusclePart.Description == result.musclePart[i]));
                var tmp = tpExercises.FirstOrDefault(x => x.ExerciseId == exercise.Id && 
                                                        x.RepsNo == result.repsNo[i] && 
                                                        x.SeriesNo == result.seriesNo[i]);

                if (tmp != null)
                {
                    tpExercises.Remove(tmp);
                    continue;
                }                   

                TrainingPlanExercise tpExercise = new TrainingPlanExercise()
                {
                    Exercise = exercise,
                    ExerciseId = exercise.Id,
                    TrainingPlanId = id,
                    TrainingPlan = plan,
                    SeriesNo = result.seriesNo[i],
                    RepsNo = result.repsNo[i]
                };

                tpExercises.Remove(tmp);
                await _context.TrainingPlanExercises.AddAsync(tpExercise);
            }

            if (tpExercises.Any())
                _context.TrainingPlanExercises.RemoveRange(tpExercises);

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public JsonResult getExercises(string trainingPartName)
        {
            List<string> exercises = _context.MusclePartExercises
                .Where(x => x.MusclePart.Description == trainingPartName)
                .Select(x => x.Exercise.Name).ToList();
            return Json(exercises);
        }
    }

    public class TrainingPlanExercises
    {
        public List<int> rowNo { get; set; }
        public List<string> musclePart { get; set; }
        public List<string> exercise { get; set; }
        public List<int> seriesNo { get; set; }
        public List<int> repsNo { get; set; }
    }
}
