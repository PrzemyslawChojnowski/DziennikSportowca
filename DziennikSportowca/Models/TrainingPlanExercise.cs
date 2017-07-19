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
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ExerciseId { get; set; }
        public virtual Exercise Exercise { get; set; }

        public int TrainingPlanId { get; set; }
        public virtual TrainingPlan TrainingPlan { get; set; }

        public int SeriesNo { get; set; }
        public int RepsNo { get; set; }
    }
}
