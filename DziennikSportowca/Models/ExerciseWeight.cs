using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class ExerciseWeight
    {
        public int Id { get; set; }
        public double Result { get; set; }

        public int UserTrainingExerciseResultId { get; set; }
        [ForeignKey("UserTrainingExerciseResultId")]
        public UserTrainingExerciseResult UserTrainingExerciseResult { get; set; }

    }
}
