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

namespace DziennikSportowca.Controllers
{
    public class UserFiguresController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserFiguresController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: UserFigures
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(await _context.UserFigure.Where(x => x.User == user).ToListAsync());
        }

        // GET: UserFigures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFigure = await _context.UserFigure
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userFigure == null)
            {
                return NotFound();
            }

            return View(userFigure);
        }

        // GET: UserFigures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserFigures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,ShouldersCircumference,ChestCircumference,WaistCircumference,BicepsCircumference,TricepsCircumference,ThighCircumference,HipCircumference,Weight,BodyFat")] UserFigure userFigure)
        {
            if (ModelState.IsValid)
            {
                userFigure.UserId = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(User));
                _context.Add(userFigure);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userFigure);
        }

        // GET: UserFigures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFigure = await _context.UserFigure.SingleOrDefaultAsync(m => m.Id == id);
            if (userFigure == null)
            {
                return NotFound();
            }
            return View(userFigure);
        }

        // POST: UserFigures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,ShouldersCircumference,ChestCircumference,WaistCircumference,BicepsCircumference,TricepsCircumference,ThighCircumference,HipCircumference,Weight,BodyFat")] UserFigure userFigure)
        {
            if (id != userFigure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFigure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFigureExists(userFigure.Id))
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
            return View(userFigure);
        }

        // GET: UserFigures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFigure = await _context.UserFigure
                .SingleOrDefaultAsync(m => m.Id == id);
            if (userFigure == null)
            {
                return NotFound();
            }

            return View(userFigure);
        }

        // POST: UserFigures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userFigure = await _context.UserFigure.SingleOrDefaultAsync(m => m.Id == id);
            _context.UserFigure.Remove(userFigure);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UserFigureExists(int id)
        {
            return _context.UserFigure.Any(e => e.Id == id);
        }

        public async Task<string> getUserGender()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);
            return user.Gender.ToString();
        }
    }
}
