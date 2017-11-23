using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Początek treningu")]
        public DateTime StartDateTime { get; set; }

        [Display(Name = "Koniec treningu")]
        public DateTime EndDateTime { get; set; }

        public virtual List<UserTrainingExerciseResult> UserTrainingsExercisesResults { get; set; }
    }
}
