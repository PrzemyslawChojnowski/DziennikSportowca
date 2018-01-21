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
    public class ExercisesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExercisesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exercises
        public async Task<IActionResult> Index()
        {
            ExercisesViewModel model = new ExercisesViewModel();

            model.CardioExercises = await _context.Exercises
                                        .Include(x => x.ActivityType)
                                        .Include(x => x.ExerciseInstruction)
                                        .Where(x => x.ActivityType.Description == "Ćwiczenia wytrzymałościowe")
                                        .OrderBy(x => x.Name)
                                        .ToListAsync();

            model.GroupExercises = await _context.Exercises
                                        .Include(x => x.ActivityType)
                                        .Include(x => x.ExerciseInstruction)
                                        .Where(x => x.ActivityType.Description == "Sporty grupowe")
                                        .OrderBy(x => x.Name)
                                        .ToListAsync();

            model.StrengthExercises = await _context.Exercises
                                        .Include(x => x.ActivityType)
                                        .Include(x => x.ExerciseInstruction)
                                        .Where(x => x.ActivityType.Description == "Ćwiczenia siłowe")
                                        .OrderBy(x => x.Name)
                                        .ToListAsync();

            return View(model);
        }

        // GET: Exercises/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.ActivityType)
                .Include(e => e.ExerciseInstruction)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // GET: Exercises/Create
        public IActionResult Create()
        {
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id");
            ViewData["ExerciseInstructionId"] = new SelectList(_context.ExerciseInstructions, "Id", "Id");
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ActivityTypeId,ExerciseInstructionId")] Exercise exercise)
        {
            if (ModelState.IsValid)
            {
                _context.Add(exercise);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", exercise.ActivityTypeId);
            ViewData["ExerciseInstructionId"] = new SelectList(_context.ExerciseInstructions, "Id", "Id", exercise.ExerciseInstructionId);
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises.Include(x => x.ActivityType).Include(x => x.ExerciseInstruction).SingleOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", exercise.ActivityTypeId);
            ViewData["ExerciseInstructionId"] = new SelectList(_context.ExerciseInstructions, "Id", "Id", exercise.ExerciseInstructionId);
            return View(exercise);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ActivityTypeId")] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Exercises.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exercise.Id))
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
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Id", exercise.ActivityTypeId);
            ViewData["ExerciseInstructionId"] = new SelectList(_context.ExerciseInstructions, "Id", "Id", exercise.ExerciseInstructionId);
            return View(exercise);
        }

        // GET: Exercises/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                .Include(e => e.ActivityType)
                .Include(e => e.ExerciseInstruction)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }

            return View(exercise);
        }

        // POST: Exercises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exercise = await _context.Exercises.SingleOrDefaultAsync(m => m.Id == id);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseExists(int id)
        {
            return _context.Exercises.Any(e => e.Id == id);
        }
    }
}
