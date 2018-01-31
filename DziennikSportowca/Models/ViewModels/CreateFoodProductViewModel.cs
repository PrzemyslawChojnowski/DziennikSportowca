using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class CreateFoodProductViewModel
    {
        public string Description { get; set; }

        [Display(Name = "Energia")]
        public double Energy { get; set; }

        [Display(Name = "Białko")]
        public double Protein { get; set; }

        [Display(Name = "Tłuszcze")]
        public double Fat { get; set; }

        [Display(Name = "Cukry")]
        public double Carbohydrate { get; set; }

        public int FoodProductTypeId { get; set; }
    }
}
