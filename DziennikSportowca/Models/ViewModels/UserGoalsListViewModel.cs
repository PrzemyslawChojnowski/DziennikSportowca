using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class UserGoalsListViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Nazwa celu")]
        public string GoalDescription { get; set; }

        [Display(Name = "Cel")]
        public string GoalScope { get; set; }

        [Display(Name = "Wartość celu")]
        public double Target { get; set; }

        [Display(Name = "Status ukończenia")]
        public bool Status { get; set; }

        [Display(Name = "Data utworzenia")]
        public DateTime CreationDate { get; set; }

        [Display(Name = "Data ukończenia")]
        public DateTime? CompletionDate { get; set; }
    }
}
