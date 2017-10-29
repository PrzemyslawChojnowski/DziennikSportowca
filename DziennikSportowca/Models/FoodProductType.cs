using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class FoodProductType
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<FoodProduct> FoodProducts { get; set; }
    }
}
