using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class UserTrainingExerciseResult
    {
        public int TrainingPlanExerciseId { get; set; }
        [ForeignKey("TrainingPlanExerciseId")]
        public TrainingPlanExercise TrainingPlanExercise { get; set; }

        public int UserTrainingId { get; set; }
        [ForeignKey("UserTrainingId")]
        public UserTraining UserTraining { get; set; }

        public int RepsNo { get; set; }
        public int SeriesNo { get; set; }
    }
}
