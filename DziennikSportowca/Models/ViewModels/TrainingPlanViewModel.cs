using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class TrainingPlanViewModel
    {
        public TrainingPlan TrainingPlan { get; set; }

        [Display(Name = "Partia mięśniowa")]
        public SelectList MuscleParts { get; set; }

        [Display(Name = "Rodzaj aktywności")]
        public SelectList ActivityTypes { get; set; }
    }
}
