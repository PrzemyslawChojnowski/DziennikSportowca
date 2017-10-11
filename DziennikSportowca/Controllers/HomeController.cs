using DziennikSportowca.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DziennikSportowca.Models;
using System.Net.

namespace DziennikSportowca.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public async Task<IActionResult> Contact(string message)
        {
            ViewData["Message"] = "";

            if (!String.IsNullOrEmpty(message))
                ViewData["Message"] = message;

            ContactViewModel model = new ContactViewModel();

            if(User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(User);
                model.Email = user.Email;
                model.Name = user.Name;
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact([Bind("Name,Email,PhoneNumber,Message")] ContactViewModel model)
        {
            //SmtpClient client = new SmtpClient("mysmtpserver");
            //client.UseDefaultCredentials = false;
            //client.Credentials = new NetworkCredential("username", "password");

            //MailMessage mailMessage = new MailMessage();
            //mailMessage.From = new MailAddress("whoever@me.com");
            //mailMessage.To.Add("receiver@me.com");
            //mailMessage.Body = "body";
            //mailMessage.Subject = "subject";
            //client.Send(mailMessage);

            return RedirectToAction("Contact", "Twoja wiadomość została wysłana");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
