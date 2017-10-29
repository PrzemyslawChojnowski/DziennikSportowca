using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class ActivityType
    {
        public ActivityType()
        {
            Exercises = new List<Exercise>();
        }

        public int Id { get; set; }

        [Display(Name = "Typ aktywności")]
        public string Description { get; set; }

        [Display(Name = "Lista ćwiczeń")]
        public virtual List<Exercise> Exercises { get; set; }
    }
}
