using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class UserGoalsViewModel
    {
        public int? Id { get; set; }
        public string UserId { get; set; }

        public SelectList ExerciseList { get; set; }

        [Required(ErrorMessage = "Pole \"Nazwa celu\" jest wymagane")]
        [Display(Name = "Nazwa celu")]
        public string GoalDescription { get; set; }

        [Display(Name = "Zakres celu")]
        public GoalScope GoalScope { get; set; }

        [Display(Name = "Cel fizyczny")]
        public PhysiqueScope PhysiqueScope { get; set; }

        [Display(Name = "Cel ćwiczeniowy")]
        public ExerciseScope ExerciseScope { get; set; }

        [Display(Name = "Część ciała")]
        public Circumferences Circumference { get; set; }

        [Display(Name = "Nazwa ćwiczenia")]
        public string ExerciseName { get; set; }

        [Required(ErrorMessage = "Pole \"Wartość celu\" jest wymagane")]
        [Display(Name = "Wartość celu")]
        public double? Target { get; set; }
    }
}
