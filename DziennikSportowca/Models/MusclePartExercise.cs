using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class MusclePartExercise
    {
        public int MuscePartId { get; set; }
        public virtual MusclePart MusclePart { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }
    }
}
