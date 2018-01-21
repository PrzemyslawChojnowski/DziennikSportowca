using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class ExercisesViewModel
    {
        public List<Exercise> GroupExercises { get; set; }
        public List<Exercise> StrengthExercises { get; set; }
        public List<Exercise> CardioExercises { get; set; }
    }
}
