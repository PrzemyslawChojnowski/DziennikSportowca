using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class SearchViewModel
    {
        public SearchViewModel()
        {
            Users = new List<ApplicationUser>();
            Exercises = new List<Exercise>();
            MuscleParts = new List<MusclePart>();
            TrainingPlans = new List<TrainingPlan>();
        }

        public string Key { get; set; }
        public string Type { get; set; }
        [Display(Name = "Użytkownicy")]
        public List<ApplicationUser> Users { get; set; }
        [Display(Name = "Ćwiczenia")]
        public List<Exercise> Exercises { get; set; }
        [Display(Name = "Partie mięśniowe")]
        public List<MusclePart> MuscleParts { get; set; }
        [Display(Name = "Plany treningowe")]
        public List<TrainingPlan> TrainingPlans { get; set; }
        [Display(Name = "Wartości odżywcze")]
        public List<FoodProduct> FoodProducts { get; set; }

    }
}
