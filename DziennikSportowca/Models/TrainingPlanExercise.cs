using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class TrainingPlanExercise
    {
        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public int TrainingPlanId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }
    }
}
