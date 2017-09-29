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
using DziennikSportowca.Models.ViewModels;

namespace DziennikSportowca.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUsersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ApplicationUsers
        public async Task<IActionResult> Index(string searchString)
        {
            var users = from u in _context.ApplicationUser select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                if(searchString.Contains(" "))
                {
                    string firstString = searchString.Substring(0, searchString.IndexOf(' '));
                    string secondString = searchString.Substring(searchString.IndexOf(' ') + 1);
                    users = users.Where(u => (u.Name == firstString && u.Surname == secondString) ||
                                        (u.Name == secondString && u.Surname == firstString));
                }
                else
                    users = users.Where(u => u.Name == searchString || u.Surname == searchString);
            }
            return View(await users.ToListAsync());
        }

        // GET: ApplicationUsers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // GET: ApplicationUsers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Surname,Gender,ProfilePicture,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicationUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }
            return View(applicationUser);
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,Surname,Gender,ProfilePicture,Id,UserName,NormalizedUserName,Email,NormalizedEmail,EmailConfirmed,PasswordHash,SecurityStamp,ConcurrencyStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEnd,LockoutEnabled,AccessFailedCount")] ApplicationUser applicationUser)
        {
            if (id != applicationUser.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicationUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(applicationUser.Id))
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
            return View(applicationUser);
        }

        // GET: ApplicationUsers/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicationUser = await _context.ApplicationUser
                .SingleOrDefaultAsync(m => m.Id == id);
            if (applicationUser == null)
            {
                return NotFound();
            }

            return View(applicationUser);
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var applicationUser = await _context.ApplicationUser.SingleOrDefaultAsync(m => m.Id == id);
            _context.ApplicationUser.Remove(applicationUser);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ApplicationUserExists(string id)
        {
            return _context.ApplicationUser.Any(e => e.Id == id);
        }

        public async Task<IActionResult> SendInvitationToFriendship(string id)
        {
            var loggedUser = await _userManager.GetUserAsync(User);
            UserFriend friendship = new UserFriend
            {
                User = loggedUser,
                FriendId = id,
                FriendshipStatus = FriendshipStatus.InvitationSent
            };
            _context.UsersFriends.Add(friendship);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Profile (string id)
        {
            if (id == null)
                return NotFound();

            var loggedUser = await _userManager.GetUserAsync(User);
            ProfileViewModel model = new ProfileViewModel();

            if (_context.UsersFriends.Where(x => loggedUser.Id == id && x.FriendId == id && 
                x.FriendshipStatus == FriendshipStatus.InvitationSent).Any())
            {
                var invitations = _context.UsersFriends.Where(x => x.FriendId == id &&
                                    x.FriendshipStatus == FriendshipStatus.InvitationSent).Include(x => x.User).ToList();
                model.Invitations = invitations;
            }
            model.User = loggedUser;

            return View(model);
        }

        public async Task<IActionResult> AcceptInvitationToFriendship (string userId, string friendId)
        {
            var invitation = await _context.UsersFriends.SingleOrDefaultAsync(x => x.FriendId == friendId && x.UserId == userId);
            invitation.FriendshipStatus = FriendshipStatus.Friends;

            _context.UsersFriends.Update(invitation);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
