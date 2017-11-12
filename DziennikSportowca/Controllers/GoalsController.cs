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
                    goalInfo = ((ExerciseScope)exerciseScope).GetDisplayName() + " (" + result.ExerciseName + "): " + goal.Target.ToString();
                }

                tmp.GoalScope = goalInfo;
                model.Add(tmp);               
            }

            var mode = _context.Goal.ToList();

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

            return View(goal);
        }

        // GET: Goals/Create
        public IActionResult Create()
        {
            UserGoalsViewModel model = new UserGoalsViewModel();
            var exercises = _context.Exercises.Select(x => x.Name);
            model.ExerciseList = new SelectList(exercises.ToList());

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
            else if((int)goal.GoalScope == 2)
            {
                ModelState.ClearValidationState("GoalScope");
                ModelState.ClearValidationState("Circumference");
                ModelState.ClearValidationState("PhysiqueScope");
                if((int)goal.ExerciseScope != 0)
                {
                    ModelState.Clear();
                }
            }

            if (!String.IsNullOrEmpty(goal.GoalDescription))
                ModelState.ClearValidationState("GoalDescription");
            if (goal.Target.HasValue)
                ModelState.ClearValidationState("Target");

            if (ModelState.IsValid)
            {
                var user = await _manager.GetUserAsync(User);
                Goal newUserGoal = new Goal()
                {
                    Description = goal.GoalDescription,
                    Target = (double)goal.Target,
                    User = user,
                    UserId = user.Id,
                    Result = false,
                    CreationDate = DateTime.Now
                };

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
                    int exerciseScope = (int)goal.GoalScope;
                    var scopes = new { GoalScope = goalScope, ExerciseScope = exerciseScope, ExerciseName = goal.ExerciseName };
                    jsonString = JsonConvert.SerializeObject(scopes, Formatting.Indented);
                    newUserGoal.Scope = jsonString;
                }
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", goal.UserId);
            return View(goal);
        }

        // POST: Goals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,Scopes,Target,Result,UserId")] Goal goal)
        {
            if (id != goal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(goal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GoalExists(goal.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", goal.UserId);
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

            return View(goal);
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
    }
}
