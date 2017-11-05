using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class Exercise
    {
        public Exercise()
        {
            TrainingPlans = new List<TrainingPlanExercise>();
            MuscleParts = new List<MusclePartExercise>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nazwa ćwiczenia")]
        public string Name { get; set; }

        public virtual List<TrainingPlanExercise> TrainingPlans { get; set; }
        [Display(Name = "Partie mięśniowe")]
        public virtual List<MusclePartExercise> MuscleParts { get; set; }

        public int ActivityTypeId { get; set; }
        [ForeignKey("ActivityTypeId")]
        public ActivityType ActivityType { get; set; }

        public int? ExerciseInstructionId { get; set; }
        [ForeignKey("ExerciseInstructionId")]
        public ExerciseInstruction ExerciseInstruction { get; set; }

    }
}
