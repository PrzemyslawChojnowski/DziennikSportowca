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
    public class UserTrainingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserTrainingsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: UserTrainings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.UserTrainings.Include(u => u.Training);
            return View(await applicationDbContext.ToListAsync());
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
    }
}
