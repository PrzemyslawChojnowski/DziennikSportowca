using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class DishFoodProduct
    {
        public int FoodProductId { get; set; }
        [ForeignKey("FoodProductId")]
        public virtual FoodProduct FoodProduct { get; set; }

        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public virtual Dish Dish { get; set; }
    }
}
