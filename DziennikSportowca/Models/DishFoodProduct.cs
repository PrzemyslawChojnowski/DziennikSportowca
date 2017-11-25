using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class DishFoodProduct
    {
        [Key]
        public int Id { get; set; }

        public int FoodProductId { get; set; }
        [ForeignKey("FoodProductId")]
        public virtual FoodProduct FoodProduct { get; set; }

        public int DishId { get; set; }
        [ForeignKey("DishId")]
        public virtual Dish Dish { get; set; }

        [Display(Name = "Waga produktu")]
        public double FoodProductWeight { get; set; }
    }
}
