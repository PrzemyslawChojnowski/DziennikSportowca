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
using Microsoft.AspNetCore.Http;
using System.IO;
using DziennikSportowca.Models.ViewModels;
using Newtonsoft.Json;

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
            return View(await _context.UserFigure.Where(x => x.User == user).OrderBy(x => x.Date).ToListAsync());
        }

        public async Task<IActionResult> Calculators()
        {            
            CalculatorsViewModel model = new CalculatorsViewModel();
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                if (_context.UserFigure.Where(x => x.User == user).Any())
                    model.Weight = await _context.UserFigure.Where(x => x.User == user).Select(x => x.Weight).LastAsync();
                model.Gender = await _context.ApplicationUser.Where(x => x.Id == user.Id).Select(x => x.Gender).SingleOrDefaultAsync();
            }
            return View(model);
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
            userFigure.Photos = await _context.Photos.Where(x => x.UserFigureId == userFigure.Id).ToListAsync();
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
        public async Task<IActionResult> Create([Bind("Id,Date,ShouldersCircumference,ChestCircumference,WaistCircumference,BicepsCircumference,TricepsCircumference,ThighCircumference,HipCircumference,Weight,BodyFat")] UserFigure userFigure, 
            List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                userFigure.UserId = await _userManager.GetUserIdAsync(await _userManager.GetUserAsync(User));
                if (files.Any())
                {
                    foreach (var ph in files)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await ph.CopyToAsync(memoryStream);
                            Photo photo = new Photo()
                            {
                                PhotoContent = memoryStream.ToArray(),
                                UserFigure = userFigure
                            };
                            _context.Photos.Add(photo);
                        }
                    }
                }
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

        public async Task<string> GetUserCircumferences(string id)
        {
            if(String.IsNullOrEmpty(id))
            {
                return null;
            }

            var circumferences = await _context.UserFigure.Where(x => x.UserId == id).OrderBy(x => x.Date).ToListAsync();
            string json = await JsonConvert.SerializeObjectAsync(circumferences);
            return json;
        }
    }
}
