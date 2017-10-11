using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class UserTraining
    {
        public UserTraining()
        {
            UserTrainingsExercisesResults = new List<UserTrainingExerciseResult>();
        }

        public int Id { get; set; }

        public int TrainingId { get; set; }
        [ForeignKey("TrainingId")]
        public TrainingPlan Training { get; set; }

        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }

        public virtual List<UserTrainingExerciseResult> UserTrainingsExercisesResults { get; set; }
    }
}
