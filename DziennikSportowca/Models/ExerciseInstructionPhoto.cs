using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class ExerciseInstructionPhoto
    {
        public int Id { get; set; }
        public string PhotoTitle { get; set; }
        public byte[] Content { get; set; }

        public int? ExerciseInstructionId { get; set; }
        [ForeignKey("ExerciseInstructionId")]
        public ExerciseInstruction ExerciseInstruction { get; set; }

    }
}
