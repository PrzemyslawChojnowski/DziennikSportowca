using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class ExerciseInstruction
    {
        public ExerciseInstruction()
        {
            Photos = new List<ExerciseInstructionPhoto>();
        }

        public int Id { get; set; }
        public string Instructions { get; set; }

        public int ExerciseId { get; set; }
        [ForeignKey("ExerciseId")]
        public Exercise Exercise { get; set; }

        public List<ExerciseInstructionPhoto> Photos { get; set; }
    }
}
