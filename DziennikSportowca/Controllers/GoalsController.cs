using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Reflection;
using Newtonsoft.Json.Converters;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.DependencyInjection;
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
    public class GoalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public GoalsController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        // GET: Goals
        public async Task<IActionResult> Index()
        {
            var userId = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));
            var dbData = await _context.Goal.Where(x => x.UserId == userId).ToListAsync();

            List<UserGoalsListViewModel> model = new List<UserGoalsListViewModel>();

            foreach (var goal in dbData)
            {
                UserGoalsListViewModel tmp = new UserGoalsListViewModel();
                tmp.CompletionDate = goal.CompletionDate;
                tmp.CreationDate = goal.CreationDate;
                tmp.GoalDescription = goal.Description;
                tmp.Status = goal.Result;
                tmp.Target = goal.Target;
                tmp.Id = goal.Id;

                string goalInfo = "";

                var result = JsonConvert.DeserializeObject<dynamic>(goal.Scope);
                var goalScope = result.GoalScope;

                if (goalScope == 1)
                {
                    var physiqueScope = result.PhysiqueScope;

                    goalInfo = ((PhysiqueScope)physiqueScope).GetDisplayName();

                    if (physiqueScope >= 1 && physiqueScope <= 4)
                    {
                        goalInfo += " do " + goal.Target.ToString();
                    }
                    else if(physiqueScope > 4 && physiqueScope <= 6)
                    {
                        var circumference = result.Circumference;
                        goalInfo += " (" + ((Circumferences)circumference).GetDisplayName() + ") do " + goal.Target.ToString(); 
                    }
                }
                else if(goalScope == 2)
                {
                    var exerciseScope = result.ExerciseScope;
                    goalInfo = ((ExerciseScope)exerciseScope).GetDisplayName() + " (";

                    if (result.ExerciseName == "All")
                        goalInfo += "Dowolne";
                    else
                        goalInfo += result.ExerciseName;

                    goalInfo += "): " + goal.Target.ToString();
                }

                tmp.GoalScope = goalInfo;
                model.Add(tmp);               
            }

            return View(model);
        }

        // GET: Goals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal
                .Include(g => g.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            string goalInfo = "";

            var result = JsonConvert.DeserializeObject<dynamic>(goal.Scope);
            var goalScope = result.GoalScope;

            if (goalScope == 1)
            {
                var physiqueScope = result.PhysiqueScope;

                goalInfo = ((PhysiqueScope)physiqueScope).GetDisplayName();

                if (physiqueScope >= 1 && physiqueScope <= 4)
                {
                    goalInfo += " do " + goal.Target.ToString();
                }
                else if (physiqueScope > 4 && physiqueScope <= 6)
                {
                    var circumference = result.Circumference;
                    goalInfo += " (" + ((Circumferences)circumference).GetDisplayName() + ") do " + goal.Target.ToString();
                }
            }
            else if (goalScope == 2)
            {
                var exerciseScope = result.ExerciseScope;
                goalInfo = ((ExerciseScope)exerciseScope).GetDisplayName() + " (";

                if (result.ExerciseName == "All")
                    goalInfo += "Dowolne";
                else
                    goalInfo += result.ExerciseName;

                goalInfo += "): " + goal.Target.ToString();
            }

            GoalDetailsViewModel model = new GoalDetailsViewModel()
            {
                Goal = goal,
                GoalInfo = goalInfo
            };

            return View(model);
        }

        // GET: Goals/Create
        public IActionResult Create()
        {
            UserGoalsViewModel model = new UserGoalsViewModel();

            return View(model);
        }

        // POST: Goals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GoalDescription,GoalScope,PhysiqueScope,ExerciseScope,Circumference,ExerciseName,Target")]
                                                    UserGoalsViewModel goal)
        {
            ModelStateDictionary modelState = ValidateModelState(goal, ModelState);

            if (modelState.IsValid)
            {
                Goal newUserGoal = await CreateNewUserGoal(goal);
                await _context.Goal.AddAsync(newUserGoal);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");

            }

            return View(goal);
        }

        // GET: Goals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal.SingleOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            if (goal.Result)
                return NotFound();

            UserGoalsViewModel model = new UserGoalsViewModel()
            {
                Target = goal.Target,
                Id = goal.Id,
                UserId = goal.UserId,
                GoalDescription = goal.Description,
            };

            var result = JsonConvert.DeserializeObject<dynamic>(goal.Scope);
            GoalScope goalScope = result.GoalScope;

            if ((int)goalScope == 1)
            {
                model.PhysiqueScope = result.PhysiqueScope;

                if(result.PhysiqueScope == 5 || result.PhysiqueScope == 6)
                    model.Circumference = result.Circumference;
            }
            else if ((int)goalScope == 2)
            {
                model.ExerciseScope = result.ExerciseScope;
                model.ExerciseName = result.ExerciseName;
            }

            model.GoalScope = goalScope;

            return View(model);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,GoalDescription,GoalScope,PhysiqueScope,ExerciseScope,Circumference,ExerciseName,Target")]
                                                    UserGoalsViewModel goal)
        {
            if (id != goal.Id)
            {
                return NotFound();
            }

            ModelStateDictionary modelState = ValidateModelState(goal, ModelState);

            if (ModelState.IsValid)
            {
                Goal newUSerGoal = await CreateNewUserGoal(goal);
                try
                {
                    _context.Update(newUSerGoal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalExists((int)newUSerGoal.Id))
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

            return View(goal);
        }

        // GET: Goals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var goal = await _context.Goal
                .Include(g => g.User)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (goal == null)
            {
                return NotFound();
            }

            string goalInfo = "";

            var result = JsonConvert.DeserializeObject<dynamic>(goal.Scope);
            var goalScope = result.GoalScope;

            if (goalScope == 1)
            {
                var physiqueScope = result.PhysiqueScope;

                goalInfo = ((PhysiqueScope)physiqueScope).GetDisplayName();

                if (physiqueScope >= 1 && physiqueScope <= 4)
                {
                    goalInfo += " do " + goal.Target.ToString();
                }
                else if (physiqueScope > 4 && physiqueScope <= 6)
                {
                    var circumference = result.Circumference;
                    goalInfo += " (" + ((Circumferences)circumference).GetDisplayName() + ") do " + goal.Target.ToString();
                }
            }
            else if (goalScope == 2)
            {
                var exerciseScope = result.ExerciseScope;
                goalInfo = ((ExerciseScope)exerciseScope).GetDisplayName() + " (";

                if (result.ExerciseName == "All")
                    goalInfo += "Dowolne";
                else
                    goalInfo += result.ExerciseName;

                goalInfo += "): " + goal.Target.ToString();
            }

            GoalDetailsViewModel model = new GoalDetailsViewModel()
            {
                Goal = goal,
                GoalInfo = goalInfo
            };

            return View(model);
        }

        // POST: Goals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var goal = await _context.Goal.SingleOrDefaultAsync(m => m.Id == id);
            _context.Goal.Remove(goal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GoalExists(int id)
        {
            return _context.Goal.Any(e => e.Id == id);
        }

        public JsonResult getExercises(string activityType)
        {
            List<string> exercises = new List<string>();

            if (!String.IsNullOrEmpty(activityType))
                exercises = _context.Exercises.Where(x => x.ActivityType.Description == activityType).Select(x => x.Name).ToList();

            return Json(exercises);
        }

        public ModelStateDictionary ValidateModelState (UserGoalsViewModel goal, ModelStateDictionary ModelState)
        {
            if (!String.IsNullOrEmpty(goal.GoalDescription))
                ModelState.ClearValidationState("GoalDescription");
            if (goal.Target.HasValue)
                ModelState.ClearValidationState("Target");

            if ((int)goal.GoalScope == 0)
            {
                ModelState.ClearValidationState("PhysiqueScope");
                ModelState.ClearValidationState("ExerciseScope");
                ModelState.ClearValidationState("Circumference");
                ModelState.ClearValidationState("ExerciseName");
            }
            else if ((int)goal.GoalScope == 1)
            {
                ModelState.ClearValidationState("GoalScope");
                ModelState.ClearValidationState("ExerciseScope");
                ModelState.ClearValidationState("ExerciseName");
                if ((int)goal.PhysiqueScope == 0)
                    ModelState.ClearValidationState("Circumference");
                else if ((int)goal.PhysiqueScope > 0 && (int)goal.PhysiqueScope < 5)
                {
                    ModelState.Clear();
                }
                else if (((int)goal.PhysiqueScope == 5 || (int)goal.PhysiqueScope == 6))
                {
                    ModelState.ClearValidationState("PhysiqueScope");
                    if ((int)goal.Circumference != 0)
                        ModelState.Clear();
                }

            }
            else if ((int)goal.GoalScope == 2)
            {
                ModelState.ClearValidationState("GoalScope");
                ModelState.ClearValidationState("Circumference");
                ModelState.ClearValidationState("PhysiqueScope");
                if ((int)goal.ExerciseScope != 0)
                {
                    ModelState.Clear();
                }
            }

            return ModelState;
        }

        public async Task<Goal> CreateNewUserGoal(UserGoalsViewModel goal)
        {
            Goal newUserGoal = new Goal()
            {
                Description = goal.GoalDescription,
                Target = (double)goal.Target,
                Result = false,
                CreationDate = DateTime.Now
            };

            if (!String.IsNullOrEmpty(goal.UserId))
            {
                newUserGoal.UserId = goal.UserId;                
            }
            else
            {
                newUserGoal.User = await _manager.GetUserAsync(User);
                newUserGoal.UserId = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));
            }

            if (goal.Id.HasValue)
                newUserGoal.Id = (int)goal.Id;

            string jsonString;

            int goalScope = (int)goal.GoalScope;
            if (goalScope == 1)
            {
                int physiqueScope = (int)goal.PhysiqueScope;
                if (physiqueScope == 5 || physiqueScope == 6)
                {
                    int circumference = (int)goal.Circumference;
                    var scopes = new { GoalScope = goalScope, PhysiqueScope = physiqueScope, Circumference = circumference };
                    jsonString = JsonConvert.SerializeObject(scopes, Formatting.Indented);
                    newUserGoal.Scope = jsonString;
                }
                else
                {
                    var scopes = new { GoalScope = goalScope, PhysiqueScope = physiqueScope };
                    jsonString = JsonConvert.SerializeObject(scopes, Formatting.Indented);
                    newUserGoal.Scope = jsonString;
                }
            }
            else if (goalScope == 2)
            {
                int exerciseScope = (int)goal.ExerciseScope;
                var scopes = new { GoalScope = goalScope, ExerciseScope = exerciseScope, ExerciseName = goal.ExerciseName };
                jsonString = JsonConvert.SerializeObject(scopes, Formatting.Indented);
                newUserGoal.Scope = jsonString;
            }

            return newUserGoal;
        }
    }
}
