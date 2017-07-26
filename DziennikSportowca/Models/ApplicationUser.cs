using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public Gender Gender { get; set; }

        public virtual List<TrainingPlan> TrainingPlans { get; set; }
        public virtual List<Dish> Dishes { get; set; }
        public virtual List<UserFigure> UserCircumferences { get; set; }
    }

    public enum Gender
    {
        [Display(Name = "Kobieta")]
        Woman = 1,

        [Display(Name = "Mężczyzna")]
        Man = 2
    }
}
