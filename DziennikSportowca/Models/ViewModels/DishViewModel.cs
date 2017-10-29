using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class DishViewModel
    {
        public Dish Dish { get; set; }

        [Display(Name = "Typ produktu")]
        public SelectList FoodProductType { get; set; }
    }
}
