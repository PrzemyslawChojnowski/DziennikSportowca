using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class NutritionFactsViewModel
    {
        public NutritionFactsViewModel()
        {
            FoodProducts = new List<FoodProduct>();
        }

        public List<FoodProduct> FoodProducts { get; set; }
        public SelectList FoodProductTypes { get; set; }
        public string type { get; set; }
    }
}
