using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class ExercisesAtlasViewModel
    {
        public string MusclePart { get; set; }
        public List<Exercise> Exercises { get; set; }
    }
}
