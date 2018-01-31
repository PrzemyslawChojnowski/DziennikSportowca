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
                                        .Include(x => x.MuscleParts)
                                            .ThenInclude(x => x.MusclePart)
                                        .Where(x => x.ActivityType.Description == "Ćwiczenia wytrzymałościowe")
                                        .OrderBy(x => x.Name)
                                        .ToListAsync();

            model.GroupExercises = await _context.Exercises
                                        .Include(x => x.ActivityType)
                                        .Include(x => x.ExerciseInstruction)
                                        .Include(x => x.MuscleParts)
                                            .ThenInclude(x => x.MusclePart)
                                        .Where(x => x.ActivityType.Description == "Sporty grupowe")
                                        .OrderBy(x => x.Name)
                                        .ToListAsync();

            model.StrengthExercises = await _context.Exercises
                                        .Include(x => x.ActivityType)
                                        .Include(x => x.ExerciseInstruction)
                                        .Include(x => x.MuscleParts)
                                            .ThenInclude(x => x.MusclePart)
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
                .Include(x => x.MuscleParts)
                    .ThenInclude(x => x.MusclePart)
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
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Description");
            ViewData["ExerciseInstructionId"] = new SelectList(_context.ExerciseInstructions, "Id", "Id");
            ViewData["MusclePartId"] = new SelectList(_context.MuscleParts, "Id", "Description");
            return View();
        }

        // POST: Exercises/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ActivityTypeId,MusclePartId")] CreateExerciseViewModel exercise)
        {
            if (ModelState.IsValid)
            {
                Exercise ex = new Exercise()
                {
                    ActivityTypeId = exercise.ActivityTypeId,
                    Name = exercise.Name
                };
                _context.Add(ex);

                _context.MusclePartExercises.Add(new MusclePartExercise()
                {
                    ExerciseId = ex.Id,
                    MuscePartId = exercise.MusclePartId
                });

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Description", exercise.ActivityTypeId);
            ViewData["MusclePartId"] = new SelectList(_context.MuscleParts, "Id", "Description", exercise.MusclePartId);
            return View(exercise);
        }

        // GET: Exercises/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exercise = await _context.Exercises
                                .Include(x => x.ActivityType)
                                .Include(x => x.ExerciseInstruction)
                                .Include(x => x.MuscleParts)
                                    .ThenInclude(x => x.MusclePart)
                                .SingleOrDefaultAsync(m => m.Id == id);
            if (exercise == null)
            {
                return NotFound();
            }
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Description", exercise.ActivityTypeId);
            ViewData["ExerciseInstructionId"] = new SelectList(_context.ExerciseInstructions, "Id", "Id", exercise.ExerciseInstructionId);
            ViewData["MusclePartId"] = new SelectList(_context.MuscleParts, "Id", "Description");

            EditExerciseViewModel model = new EditExerciseViewModel()
            {
                ActivityTypeId = exercise.ActivityTypeId,
                Id = exercise.Id,
                Name = exercise.Name
            };

            if (exercise.MuscleParts != null && exercise.MuscleParts.Any())
                model.MusclePartId = exercise.MuscleParts[0].MuscePartId;

            return View(model);
        }

        // POST: Exercises/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ActivityTypeId,MusclePartId")] EditExerciseViewModel exerciseModel)
        {
            if (id != exerciseModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Exercise exercise = await _context.Exercises
                                    .Include(x => x.MuscleParts)
                                        .ThenInclude(x => x.MusclePart)
                                    .Include(x => x.ExerciseInstruction)
                                    .Include(x => x.ActivityType)
                                    .SingleOrDefaultAsync(x => x.Id == id);

                    if(exercise != null)
                    {
                        exercise.ActivityTypeId = exerciseModel.ActivityTypeId;
                        exercise.Name = exerciseModel.Name;

                        if (exercise.MuscleParts != null && exercise.MuscleParts.Any())
                        {
                            if (exercise.MuscleParts[0].MuscePartId != exerciseModel.MusclePartId)
                            {
                                MusclePartExercise mpExercise = await _context.MusclePartExercises.FirstOrDefaultAsync(x => x.MuscePartId == exercise.MuscleParts[0].MuscePartId && x.ExerciseId == id);
                                _context.MusclePartExercises.Remove(mpExercise);
                                await _context.SaveChangesAsync();
                                _context.MusclePartExercises.Add(new MusclePartExercise()
                                {
                                    ExerciseId = exercise.Id,
                                    MuscePartId = exerciseModel.MusclePartId
                                });
                                await _context.SaveChangesAsync();
                            }
                        }
                        else
                        {
                            _context.MusclePartExercises.Add(new MusclePartExercise()
                            {
                                ExerciseId = exercise.Id,
                                MuscePartId = exerciseModel.MusclePartId
                            });
                            await _context.SaveChangesAsync();
                        }
                    }
                    _context.Exercises.Update(exercise);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseExists(exerciseModel.Id))
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
            ViewData["ActivityTypeId"] = new SelectList(_context.ActivityTypes, "Id", "Description", exerciseModel.ActivityTypeId);
            ViewData["MusclePartId"] = new SelectList(_context.MuscleParts, "Id", "Description");

            EditExerciseViewModel model = new EditExerciseViewModel()
            {
                ActivityTypeId = exerciseModel.ActivityTypeId,
                Id = exerciseModel.Id,
                Name = exerciseModel.Name,
                MusclePartId = exerciseModel.MusclePartId
            };

            return View(model);
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
                .Include(e => e.MuscleParts)
                    .ThenInclude(e => e.MusclePart)
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
