using System.ComponentModel.DataAnnotations.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class Goal
    {
        public Goal()
        {
            Result = false;
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Scope { get; set; }
        public double Target { get; set; }
        public bool Result { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? CompletionDate { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
    }
}
