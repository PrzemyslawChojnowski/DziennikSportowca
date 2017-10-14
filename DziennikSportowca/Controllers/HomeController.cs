﻿using Microsoft.EntityFrameworkCore;
using DziennikSportowca.Data;
using DziennikSportowca.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DziennikSportowca.Models;
using MimeKit;
using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Routing;

namespace DziennikSportowca.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private IConfiguration _configuration { get; set; }
        private readonly ApplicationDbContext _context;

        public HomeController(UserManager<ApplicationUser> userManager, IConfiguration configuration, ApplicationDbContext context)
        {
            _userManager = userManager;
            _configuration = configuration;
            _context = context;
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

        public async Task<IActionResult> Contact(string infoMessage, string name, string phoneNumber, string email, string message)
        {
            ContactViewModel model = new ContactViewModel();
            ViewData["InfoMessage"] = "";

            if (!String.IsNullOrEmpty(infoMessage))
                ViewData["InfoMessage"] = infoMessage;
            
            if(!String.IsNullOrEmpty(name) || !String.IsNullOrEmpty(phoneNumber) || !String.IsNullOrEmpty(email) || !String.IsNullOrEmpty(message))
            {
                model.Name = name;
                model.PhoneNumber = phoneNumber;
                model.Email = email;
                model.Message = message;
            }
            else if (User.Identity.IsAuthenticated)
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
            MailMessage msg = new MailMessage();
            SmtpClient client = new SmtpClient();
            try
            {
                msg.Subject = "Wiadomość kontaktowa";
                string message = "Imię: " + model.Name + "<br/>" + 
                                 "Nr tel.: " + model.PhoneNumber + "<br/>" + 
                                 "E-mail: " + model.Email + "<br/>" +
                                 "Wiadomość: " + model.Message;
                msg.Body = message;
                msg.From = new MailAddress(model.Email);
                msg.To.Add(_configuration["MyEmail"]);
                msg.IsBodyHtml = true;
                client.Host = "smtp.gmail.com";
                NetworkCredential basicAuthenticationInfo = new NetworkCredential(_configuration["MyEmai"], _configuration["MyPassword"]);
                client.Port = int.Parse("587");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicAuthenticationInfo;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(msg);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Contact", new
                {
                    infoMessage = "Niestety, ale Twoja wiadomość nie została wysłana. \nProsimy, abyś ponowił próbę.",
                    name = model.Name,
                    phoneNumber = model.PhoneNumber,
                    email = model.Email,
                    message = model.Message
                });
            }

            return RedirectToAction("Contact", new { message = "Twoja wiadomość została wysłana" });
        }

        public async Task<IActionResult> Search(string key)
        {
            SearchViewModel model = new SearchViewModel();

            if (!String.IsNullOrEmpty(key))
            {
                
                model.Key = key;
                model.Exercises = await _context.Exercises.Where(x => x.Name.Contains(key)).ToListAsync();
                model.MuscleParts = await _context.MuscleParts.Where(x => x.Description.Contains(key)).ToListAsync();
                model.TrainingPlans = await _context.TrainingPlans.Where(x => x.Description.Contains(key)).ToListAsync();
                model.Users = await _context.ApplicationUser
                          .Where(x => x.UserName.Contains(key) || x.Surname.Contains(key) || x.Name.Contains(key) || x.Email.Contains(key)).ToListAsync();
   
            }
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
