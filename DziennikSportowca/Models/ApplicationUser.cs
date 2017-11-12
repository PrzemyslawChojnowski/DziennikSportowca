using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace DziennikSportowca.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            TrainingPlans = new List<TrainingPlan>();
            Dishes = new List<Dish>();
            UserCircumferences = new List<UserFigure>();
            UserFriends = new List<UserFriend>();
            Users = new List<UserFriend>();
            Goals = new List<Goal>();
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }
        public byte[] ProfilePicture { get; set; } 

        public virtual List<TrainingPlan> TrainingPlans { get; set; }
        public virtual List<Dish> Dishes { get; set; }
        public virtual List<UserFigure> UserCircumferences { get; set; }
        public virtual List<UserFriend> UserFriends { get; set; }
        public virtual List<UserFriend> Users { get; set; }
        public virtual List<Goal> Goals { get; set; }
    }  
}
