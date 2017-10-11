using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Authorization;
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
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DziennikSportowca.Controllers
{
    public class UserTrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        
        public UserTrainingsController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: UserTrainings
        public async Task<IActionResult> Index()
        {
            string loggedUserId = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));
            var trainings = _context.UserTrainings.
                Where(x => x.Training.UserId == loggedUserId).
                Include(x => x.Training).
                Include(x => x.UserTrainingsExercisesResults);

            return View(await trainings.ToListAsync());
        }

        // GET: UserTrainings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTraining = await _context.UserTrainings
                .Include(u => u.Training)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userTraining == null)
            {
                return NotFound();
            }

            return View(userTraining);
        }

        // GET: UserTrainings/Create
        public IActionResult Create()
        {
            ViewData["TrainingId"] = new SelectList(_context.TrainingPlans, "Id", "Id");
            return View();
        }

        // POST: UserTrainings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrainingId,TrainingDate")] UserTraining userTraining)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userTraining);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["TrainingId"] = new SelectList(_context.TrainingPlans, "Id", "Id", userTraining.TrainingId);
            return View(userTraining);
        }

        // GET: UserTrainings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTraining = await _context.UserTrainings.SingleOrDefaultAsync(m => m.Id == id);
            if (userTraining == null)
            {
                return NotFound();
            }
            ViewData["TrainingId"] = new SelectList(_context.TrainingPlans, "Id", "Id", userTraining.TrainingId);
            return View(userTraining);
        }

        // POST: UserTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TrainingId,TrainingDate")] UserTraining userTraining)
        {
            if (id != userTraining.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userTraining);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserTrainingExists(userTraining.Id))
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
            ViewData["TrainingId"] = new SelectList(_context.TrainingPlans, "Id", "Id", userTraining.TrainingId);
            return View(userTraining);
        }

        // GET: UserTrainings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userTraining = await _context.UserTrainings
                .Include(u => u.Training)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userTraining == null)
            {
                return NotFound();
            }

            return View(userTraining);
        }

        // POST: UserTrainings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userTraining = await _context.UserTrainings.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserTrainings.Remove(userTraining);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserTrainingExists(int id)
        {
            return _context.UserTrainings.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DoTheWorkout(int? id)
        {
            var selectedTraining = await _context.TrainingPlans.FirstOrDefaultAsync(x => x.Id == id);

            List<TrainingPlanExercise> exercises = await _context.TrainingPlanExercises.
                                                    Where(x => x.TrainingPlanId == selectedTraining.Id).
                                                    Include(x => x.Exercise).
                                                    Include(x => x.Exercise.MuscleParts).
                                                    ToListAsync();

            foreach (var exercise in exercises)
            {
                exercise.Exercise.MuscleParts = await _context.MusclePartExercises.Where(x => x.ExerciseId == exercise.ExerciseId).Include(x => x.MusclePart).ToListAsync();
            }

            TrainingViewModel model = new TrainingViewModel();
            selectedTraining.Exercises = exercises;
            model.TrainingPlan = selectedTraining;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTrainingResults(string jsonString, int id, string startDate, string endDate)
        {
            DateTime localStartDateTime = ((DateTime)JsonConvert.DeserializeObject(startDate)).ToLocalTime();
            DateTime localEndDateTime = ((DateTime)JsonConvert.DeserializeObject(endDate)).ToLocalTime();

            var deserializedJsonData = JsonConvert.DeserializeAnonymousType(jsonString, 
                new[] {
                    new { Exercise = "",
                        SeriesNo = 0,
                        RepsNo = 0,
                        Weight = new List<double>() }
                }.ToList());
           
            TrainingPlan tp = await _context.TrainingPlans.FirstOrDefaultAsync(x => x.Id == id);

            if(tp == null)
            {
                return NotFound();
            }

            UserTraining training = new UserTraining()
            {
                Training = tp,
                TrainingId = tp.Id,
                StartDateTime = localStartDateTime,
                EndDateTime = localEndDateTime
            };

            List<UserTrainingExerciseResult> results = new List<UserTrainingExerciseResult>();

            foreach (var exercise in deserializedJsonData)
            {
                TrainingPlanExercise tpExercise = await _context.TrainingPlanExercises.FirstOrDefaultAsync
                    (x => x.TrainingPlanId == tp.Id);

                if(tpExercise == null)
                {
                    return NotFound();
                }

                UserTrainingExerciseResult result = new UserTrainingExerciseResult()
                {
                    UserTraining = training,
                    UserTrainingId = training.Id,
                    RepsNo = exercise.RepsNo,
                    SeriesNo = exercise.SeriesNo,
                    TrainingPlanExercise = tpExercise,
                    TrainingPlanExerciseId = tpExercise.Id
                };

                List<ExerciseWeight> weights = new List<ExerciseWeight>();

                foreach (var weightResult in exercise.Weight)
                {
                    ExerciseWeight weight = new ExerciseWeight()
                    {
                        UserTrainingExerciseResult = result,
                        UserTrainingExerciseResultId = result.Id,
                        Weight = weightResult
                    };
                    weights.Add(weight);                   
                }

                _context.ExercisesWeights.AddRange(weights);
                result.Weights = weights;
                results.Add(result);
            }
            _context.UserTrainingExercisesResults.AddRange(results);
            training.UserTrainingsExercisesResults = results;
            _context.UserTrainings.Add(training);
            _context.SaveChanges();

            return new EmptyResult();
        }
    }
}
