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
                    .ThenInclude(x => x.Exercises)
                        .ThenInclude(x => x.Exercise)
                            .ThenInclude(x => x.ActivityType)
                .Include(x => x.UserTrainingsExercisesResults)
                    .ThenInclude(x => x.Weights)
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
                                                .ThenInclude(x => x.MuscleParts)
                                                    .ThenInclude(x => x.MusclePart)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.TrainingPlanExercise)
                                            .ThenInclude(x => x.Exercise)
                                                .ThenInclude(x => x.ActivityType)
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
        public async Task<IActionResult> Edit(string jsonString, int id, int trainingId, string startDate, string endDate)
        {
            DateTime localStartDateTime = ((DateTime)JsonConvert.DeserializeObject(startDate)).ToLocalTime();
            DateTime localEndDateTime = ((DateTime)JsonConvert.DeserializeObject(endDate)).ToLocalTime();

            var trainingToUpdate = await _context.UserTrainings.Where(x => x.Id == trainingId)
                                    .Include(x => x.Training)
                                        .ThenInclude(x => x.Exercises)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.TrainingPlanExercise)
                                            .ThenInclude(x => x.Exercise)
                                                .ThenInclude(x => x.MuscleParts)
                                                    .ThenInclude(x => x.MusclePart)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.TrainingPlanExercise)
                                            .ThenInclude(x => x.Exercise)
                                                .ThenInclude(x => x.ActivityType)
                                    .Include(x => x.UserTrainingsExercisesResults)
                                        .ThenInclude(x => x.Weights).
                                    FirstOrDefaultAsync();

            var deserializedJsonData = JsonConvert.DeserializeAnonymousType(jsonString,
                new[] {
                    new { Exercise = "",
                        ActivityType = "",
                        ExerciseInfo = new { RepsNo = 0, SeriesNo = 0, Weight = new List<int>(), ExerciseLength = 0, ExerciseLengthAtTraining = 0 } }
                }.ToList());
            int i = 0;

            foreach (var exercise in deserializedJsonData)
            {
                if (exercise.ActivityType == "Æwiczenia si³owe")
                {
                    int j = 0;
                    foreach (var weightResult in exercise.ExerciseInfo.Weight)
                    {
                        trainingToUpdate.UserTrainingsExercisesResults[i].Weights[j].Result = weightResult;
                        j++;
                    }
                }
                else if (exercise.ActivityType == "Æwiczenia wytrzyma³oœciowe" || exercise.ActivityType == "Sporty grupowe")
                {
                    trainingToUpdate.UserTrainingsExercisesResults[i].Weights[0].Result = exercise.ExerciseInfo.ExerciseLengthAtTraining;
                }
                i++;
            }

            trainingToUpdate.StartDateTime = localStartDateTime;
            trainingToUpdate.EndDateTime = localEndDateTime;

            _context.UserTrainings.Update(trainingToUpdate);
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
                    .ThenInclude(x => x.Exercises)
                        .ThenInclude(x => x.Exercise)
                            .ThenInclude(x => x.ActivityType)
                .Include(x => x.UserTrainingsExercisesResults)
                    .ThenInclude(x => x.Weights)
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
                                                        ThenInclude(x => x.ActivityType).
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
            var functionResult = await CreateUserTrainingObjectInstance(jsonString, id, startDate, endDate);

            if (functionResult.Item1 == null || functionResult.Item2 == null)
                return NotFound();

            _context.UserTrainingExercisesResults.AddRange(functionResult.Item2);
            functionResult.Item1.UserTrainingsExercisesResults = functionResult.Item2;

            _context.UserTrainings.Add(functionResult.Item1);
            _context.SaveChanges();

            var userId = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));
            var userGoals = _context.Goal.Where(x => x.UserId == userId && x.Result == false && x.CreationDate < functionResult.Item1.EndDateTime);

            if (userGoals != null && userGoals.Any())
            {
                foreach (var goal in userGoals)
                {
                    var result = JsonConvert.DeserializeObject<dynamic>(goal.Scope);
                    var goalScope = result.GoalScope;
                    bool successFlag = false;

                    if (goalScope == 2)
                    {
                        var exerciseScope = result.ExerciseScope;
                        var exercise = result.ExerciseName.ToString();

                        if (exerciseScope == 1)
                        {
                            if (exercise == "All")
                            {
                                var actualTrainingGroupSports = functionResult.Item2.Where(x => x.TrainingPlanExercise.Exercise.ActivityType.Description == "Sporty grupowe").ToList();

                                if (actualTrainingGroupSports != null && actualTrainingGroupSports.Any())
                                {
                                    foreach (var actualTrainingExercise in actualTrainingGroupSports)
                                    {
                                        if (actualTrainingExercise.Weights.Any(x => x.Result >= goal.Target))
                                            successFlag = true;
                                    }
                                }
                            }
                            else if (exercise != null)
                            {
                                var actualTrainingExercises = functionResult.Item2.Where(x => x.TrainingPlanExercise.Exercise.Name == (string)exercise &&
                                                                                            x.TrainingPlanExercise.Exercise.ActivityType.Description == "Sporty grupowe").ToList();
                                if (actualTrainingExercises != null && actualTrainingExercises.Any())
                                {
                                    foreach (var actualExercise in actualTrainingExercises)
                                    {
                                        if (actualExercise.Weights.Any(x => x.Result >= goal.Target))
                                            successFlag = true;
                                    }
                                }
                            }
                        }
                        else if(exerciseScope == 2)
                        {
                            if(exercise == "All")
                            {
                                var resultsSum = _context.ExercisesWeights
                                                .Include(x => x.UserTrainingExerciseResult)
                                                    .ThenInclude(x => x.TrainingPlanExercise)
                                                        .ThenInclude(x => x.Exercise)
                                                            .ThenInclude(x => x.ActivityType)
                                                .Include(x => x.UserTrainingExerciseResult.UserTraining)
                                                    .ThenInclude(x => x.Training)
                                                        .ThenInclude(x => x.User)
                                                .Where(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.ActivityType.Description == "Sporty grupowe"
                                                    && x.UserTrainingExerciseResult.UserTraining.Training.UserId == userId)
                                                .Select(x => x.Result)
                                                .Sum();

                                if (resultsSum >= goal.Target)
                                    successFlag = true;
                            }
                            else if(exercise != null)
                            {
                                string exerciseName = exercise.ToString();

                                var resultsSum = _context.ExercisesWeights
                                                .Include(x => x.UserTrainingExerciseResult)
                                                    .ThenInclude(x => x.TrainingPlanExercise)
                                                        .ThenInclude(x => x.Exercise)
                                                            .ThenInclude(x => x.ActivityType)
                                                .Include(x => x.UserTrainingExerciseResult.UserTraining)
                                                    .ThenInclude(x => x.Training)
                                                        .ThenInclude(x => x.User)
                                                .Where(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.ActivityType.Description == "Sporty grupowe"
                                                    && x.UserTrainingExerciseResult.UserTraining.Training.UserId == userId
                                                    && x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.Name == exerciseName)
                                                .Select(x => x.Result)
                                                .Sum();

                                if (resultsSum >= goal.Target)
                                    successFlag = true;
                            }
                        }
                        else if (exerciseScope == 3)
                        {
                            if (exercise == "All")
                            {
                                var actualTrainingGroupSports = functionResult.Item2.Where(x => x.TrainingPlanExercise.Exercise.ActivityType.Description == "Æwiczenia wytrzyma³oœciowe").ToList();

                                if (actualTrainingGroupSports != null && actualTrainingGroupSports.Any())
                                {
                                    foreach (var actualTrainingExercise in actualTrainingGroupSports)
                                    {
                                        if (actualTrainingExercise.Weights.Any(x => x.Result >= goal.Target))
                                            successFlag = true;
                                    }
                                }
                            }
                            else if (exercise != null)
                            {
                                var actualTrainingExercises = functionResult.Item2.Where(x => x.TrainingPlanExercise.Exercise.Name == (string)exercise &&
                                                                                            x.TrainingPlanExercise.Exercise.ActivityType.Description == "Æwiczenia wytrzyma³oœciowe").ToList();
                                if (actualTrainingExercises != null && actualTrainingExercises.Any())
                                {
                                    foreach (var actualExercise in actualTrainingExercises)
                                    {
                                        if (actualExercise.Weights.Any(x => x.Result >= goal.Target))
                                            successFlag = true;
                                    }
                                }
                            }
                        }
                        else if (exerciseScope == 4)
                        {
                            if (exercise == "All")
                            {
                                var resultsSum = _context.ExercisesWeights
                                                .Include(x => x.UserTrainingExerciseResult)
                                                    .ThenInclude(x => x.TrainingPlanExercise)
                                                        .ThenInclude(x => x.Exercise)
                                                            .ThenInclude(x => x.ActivityType)
                                                .Include(x => x.UserTrainingExerciseResult.UserTraining)
                                                    .ThenInclude(x => x.Training)
                                                        .ThenInclude(x => x.User)
                                                .Where(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.ActivityType.Description == "Æwiczenia wytrzyma³oœciowe"
                                                    && x.UserTrainingExerciseResult.UserTraining.Training.UserId == userId)
                                                .Select(x => x.Result)
                                                .Sum();

                                if (resultsSum >= goal.Target)
                                    successFlag = true;
                            }
                            else if (exercise != null)
                            {
                                string exerciseName = exercise.ToString();

                                var resultsSum = _context.ExercisesWeights
                                                .Include(x => x.UserTrainingExerciseResult)
                                                    .ThenInclude(x => x.TrainingPlanExercise)
                                                        .ThenInclude(x => x.Exercise)
                                                            .ThenInclude(x => x.ActivityType)
                                                .Include(x => x.UserTrainingExerciseResult.UserTraining)
                                                    .ThenInclude(x => x.Training)
                                                        .ThenInclude(x => x.User)
                                                .Where(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.ActivityType.Description == "Æwiczenia wytrzyma³oœciowe"
                                                    && x.UserTrainingExerciseResult.UserTraining.Training.UserId == userId
                                                    && x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.Name == exerciseName)
                                                .Select(x => x.Result)
                                                .Sum();

                                if (resultsSum >= goal.Target)
                                    successFlag = true;
                            }
                        }
                        else if (exerciseScope == 5)
                        {
                            if (exercise == "All")
                            {
                                var actualTrainingGroupSports = functionResult.Item2.Where(x => x.TrainingPlanExercise.Exercise.ActivityType.Description == "Æwiczenia si³owe").ToList();

                                if (actualTrainingGroupSports != null && actualTrainingGroupSports.Any())
                                {
                                    foreach (var actualTrainingExercise in actualTrainingGroupSports)
                                    {
                                        if (actualTrainingExercise.Weights.Any(x => x.Result >= goal.Target))
                                            successFlag = true;
                                    }
                                }
                            }
                            else if (exercise != null)
                            {
                                var actualTrainingExercises = functionResult.Item2.Where(x => x.TrainingPlanExercise.Exercise.Name == (string)exercise &&
                                                                                            x.TrainingPlanExercise.Exercise.ActivityType.Description == "Æwiczenia si³owe").ToList();
                                if (actualTrainingExercises != null && actualTrainingExercises.Any())
                                {
                                    foreach (var actualExercise in actualTrainingExercises)
                                    {
                                        if (actualExercise.Weights.Any(x => x.Result >= goal.Target))
                                            successFlag = true;
                                    }
                                }
                            }
                        }
                    }
                    if(successFlag)
                    {
                        try
                        {
                            goal.Result = true;
                            goal.CompletionDate = functionResult.Item1.EndDateTime;
                            _context.Goal.Update(goal);                            
                        }
                        catch(Exception ex)
                        {
                            Console.WriteLine("Error updating goal result to true. Error: " + ex.Message);
                        }
                    }
                }
            }
            _context.SaveChanges();
            return new EmptyResult();
        }

        public async Task<(UserTraining,List<UserTrainingExerciseResult>)> CreateUserTrainingObjectInstance(string jsonString, int id, string startDate, string endDate)
        {
            DateTime localStartDateTime = ((DateTime)JsonConvert.DeserializeObject(startDate)).ToLocalTime();
            DateTime localEndDateTime = ((DateTime)JsonConvert.DeserializeObject(endDate)).ToLocalTime();

            var deserializedJsonData = JsonConvert.DeserializeAnonymousType(jsonString,
                new[] {
                    new { Exercise = "",
                        ActivityType = "",
                        ExerciseInfo = new { RepsNo = 0, SeriesNo = 0, Weight = new List<int>(), ExerciseLength = 0, ExerciseLengthAtTraining = 0 } }
                }.ToList());

            TrainingPlan tp = await _context.TrainingPlans.FirstOrDefaultAsync(x => x.Id == id);

            if (tp == null)
            {
                return (null,null);
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
                tpExercise.Exercise = await _context.Exercises.Include(X => X.ActivityType).FirstOrDefaultAsync(x => x.Id == tpExercise.ExerciseId);

                if (tpExercise == null)
                {
                    return (null, null);
                }

                UserTrainingExerciseResult result = new UserTrainingExerciseResult();
                List<ExerciseWeight> weights = new List<ExerciseWeight>();

                if (exercise.ActivityType == "Æwiczenia si³owe")
                {
                    var tmp = new UserTrainingExerciseResult()
                    {
                        UserTraining = training,
                        UserTrainingId = training.Id,
                        RepsNo = exercise.ExerciseInfo.RepsNo,
                        SeriesNo = exercise.ExerciseInfo.SeriesNo,
                        TrainingPlanExercise = tpExercise,
                        TrainingPlanExerciseId = tpExercise.Id
                    };
                    result = tmp;

                    foreach (var weightResult in exercise.ExerciseInfo.Weight)
                    {
                        ExerciseWeight weight = new ExerciseWeight()
                        {
                            UserTrainingExerciseResult = result,
                            UserTrainingExerciseResultId = result.Id,
                            Result = weightResult
                        };
                        weights.Add(weight);
                    }
                }
                else if (exercise.ActivityType == "Æwiczenia wytrzyma³oœciowe" || exercise.ActivityType == "Sporty grupowe")
                {
                    var tmp = new UserTrainingExerciseResult()
                    {
                        UserTraining = training,
                        UserTrainingId = training.Id,
                        RepsNo = exercise.ExerciseInfo.RepsNo,
                        SeriesNo = exercise.ExerciseInfo.SeriesNo,
                        TrainingPlanExercise = tpExercise,
                        TrainingPlanExerciseId = tpExercise.Id
                    };
                    result = tmp;

                    ExerciseWeight weight = new ExerciseWeight()
                    {
                        UserTrainingExerciseResult = result,
                        UserTrainingExerciseResultId = result.Id,
                        Result = exercise.ExerciseInfo.ExerciseLengthAtTraining
                    };
                    weights.Add(weight);
                }

                _context.ExercisesWeights.AddRange(weights);
                result.Weights = weights;
                results.Add(result);
            }

            return (training,results);
        }
    }
}
