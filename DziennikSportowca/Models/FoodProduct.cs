using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class FoodProduct
    {
        public FoodProduct()
        {
            Dishes = new List<DishFoodProduct>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        [Display(Name = "Miara")]
        public Measurement Measurement { get; set; }

        [Display(Name = "Energia")]
        public double Energy { get; set; }

        [Display(Name = "Białko")]
        public double Protein { get; set; }

        [Display(Name = "Tłuszcze")]
        public double Fat { get; set; }

        [Display(Name = "Cukry")]
        public double Carbohydrate { get; set; }

        public virtual List<DishFoodProduct> Dishes { get; set; } 
    }

    public enum Measurement
    {
        Volume = 1,
        Weight = 2
    }
}
