using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class FoodProductsViewModel
    {
        public List<FoodProduct> MilkEggsProducts { get; set; }
        public List<FoodProduct> CerealProducts { get; set; }
        public List<FoodProduct> MeatProducts { get; set; }
        public List<FoodProduct> Sweets { get; set; }
        public List<FoodProduct> Bakals { get; set; }
        public List<FoodProduct> Drinks { get; set; }
        public List<FoodProduct> Fruits { get; set; }
        public List<FoodProduct> PreparedProducts { get; set; }
        public List<FoodProduct> Fishes { get; set; }
        public List<FoodProduct> Fats { get; set; }
        public List<FoodProduct> Vegetables { get; set; }
    }
}
