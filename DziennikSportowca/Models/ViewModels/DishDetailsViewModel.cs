using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class DishDetailsViewModel
    {
        public Dish Dish { get; set; }

        [Display(Name = "Węglowodany")]
        public double TotalCarbs { get; set; }

        [Display(Name = "Tłuszcze")]
        public double TotalFat { get; set; }

        [Display(Name = "Białko")]
        public double TotalProteins { get; set; }

        [Display(Name = "Kaloryczność")]
        public double TotalEnergy { get; set; }

        [Display(Name = "Waga")]
        public double TotalWeight { get; set; }
    }
}
