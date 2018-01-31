using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class CreateExerciseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa ćwiczenia")]
        public string Name { get; set; }

        [Display(Name = "Typ aktywności")]
        public int ActivityTypeId { get; set; }

        [Display(Name = "Partia mięśniowa")]
        public int MusclePartId { get; set; }
    }
}
