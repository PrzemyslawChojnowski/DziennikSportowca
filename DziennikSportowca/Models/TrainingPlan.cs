using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class TrainingPlan
    {
        public TrainingPlan()
        {
            Exercises = new List<TrainingPlanExercise>();
            UserTrainings = new List<UserTraining>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nazwa planu")]
        public string Description { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        public virtual List<TrainingPlanExercise> Exercises { get; set; }
        public virtual List<UserTraining> UserTrainings { get; set; }
    }
}
