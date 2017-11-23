using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class TrainingPlanExercise
    {
        public TrainingPlanExercise()
        {
            Results = new List<UserTrainingExerciseResult>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Index { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public int TrainingPlanId { get; set; }
        [ForeignKey("TrainingPlanId")]
        public virtual TrainingPlan TrainingPlan { get; set; }

        public virtual List<UserTrainingExerciseResult> Results { get; set; }

        public int? SeriesNo { get; set; }
        public int? RepsNo { get; set; }
        public int? ExerciseLength { get; set; }
    }
}
