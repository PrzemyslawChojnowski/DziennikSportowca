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
            var loggedUserId = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));

            var selectedTraining = await _context.UserTrainings.Where(x => x.Id == id)
                                    .Include(x => x.Training)
                                        .ThenInclude(x => x.Exercises)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.TrainingPlanExercise)
                                        .ThenInclude(x => x.Exercise)
                                        .ThenInclude(x => x.MuscleParts).ThenInclude(x => x.MusclePart)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.Weights)
                                    .FirstOrDefaultAsync(x => x.Training.UserId == loggedUserId);

            if (selectedTraining == null)
                return NotFound();

            return View(selectedTraining);
        }

        // POST: UserTrainings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string jsonString, int id, string startDate, string endDate)
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

            if(!_context.UserTrainings.Any(x => x.Id == id))
            {
                return NotFound();
            }

            UserTraining userTraining = await _context.UserTrainings.FirstOrDefaultAsync(x => x.Id == id);
            userTraining.UserTrainingsExercisesResults = await _context.UserTrainingExercisesResults
                                                        .Where(x => x.UserTrainingId == id)
                                                        .Include(x => x.Weights)
                                                        .Include(x => x.TrainingPlanExercise)
                                                            .ThenInclude(x => x.Exercise)
                                                        .ToListAsync();

            userTraining.StartDateTime = localStartDateTime;
            userTraining.EndDateTime = localEndDateTime;
            int i = 0;
            foreach(var exercise in deserializedJsonData)
            {
                var index = userTraining.UserTrainingsExercisesResults.FindIndex(x => exercise.Exercise == x.TrainingPlanExercise.Exercise.Name);
                userTraining.UserTrainingsExercisesResults.ElementAt(index).TrainingPlanExercise.RepsNo = exercise.RepsNo;
                userTraining.UserTrainingsExercisesResults.ElementAt(index).TrainingPlanExercise.SeriesNo = exercise.SeriesNo;
                for (int j = 0; j < exercise.Weight.Count; j++)
                    userTraining.UserTrainingsExercisesResults.ElementAt(index).Weights[j].Weight = exercise.Weight[j];
                i++;
            }

            _context.UserTrainings.Update(userTraining);
            _context.SaveChanges();

            return new EmptyResult();
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
            var loggedUserId = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));

            var selectedTraining = await _context.UserTrainings.Where(x => x.Id == id)
                                    .Include(x => x.Training)
                                        .ThenInclude(x => x.Exercises)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.TrainingPlanExercise)
                                        .ThenInclude(x => x.Exercise)
                                        .ThenInclude(x => x.MuscleParts).ThenInclude(x => x.MusclePart)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.Weights)
                                    .FirstOrDefaultAsync(x => x.Training.UserId == loggedUserId);

            if (selectedTraining == null)
                return NotFound();

            foreach (var result in selectedTraining.UserTrainingsExercisesResults)
                _context.ExercisesWeights.RemoveRange(result.Weights);
            _context.UserTrainingExercisesResults.RemoveRange(selectedTraining.UserTrainingsExercisesResults);
            _context.UserTrainings.Remove(selectedTraining);

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

            if (tp == null)
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
                    (x => x.TrainingPlanId == tp.Id && x.Exercise.Name == exercise.Exercise);
                tpExercise.Exercise = await _context.Exercises.FirstOrDefaultAsync(x => x.Id == tpExercise.ExerciseId);

                if (tpExercise == null)
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
