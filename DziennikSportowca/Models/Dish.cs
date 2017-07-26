using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class Dish
    {
        public Dish()
        {
            FoodProducts = new List<DishFoodProduct>();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public virtual List<DishFoodProduct> FoodProducts { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }
}
