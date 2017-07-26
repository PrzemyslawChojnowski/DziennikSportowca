using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public class UserFigure
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "Data pomiaru")]
        public DateTime Date { get; set; }

        [Display(Name = "Obwód w barkach [cm]")]
        public double ShouldersCircumference { get; set; }

        [Display(Name = "Obwód klatki piersiowej [cm]")]
        public double ChestCircumference { get; set; }

        [Display(Name = "Obwód talii [cm]")]
        public double WaistCircumference { get; set; }

        [Display(Name = "Obwód bicepsa [cm]")]
        public double BicepsCircumference { get; set; }

        [Display(Name = "Obwód tricepsa [cm]")]
        public double TricepsCircumference { get; set; }

        [Display(Name = "Obwód uda [cm]")]
        public double ThighCircumference { get; set; }

        [Display(Name = "Obwód bioder [cm]")]
        public double HipCircumference { get; set; }

        [Display(Name = "Waga [kg]")]
        public double Weight { get; set; }

        [Display(Name = "Poziom tkanki tłuszczowej [%]")]
        public double BodyFat { get; set; }       
    }
}
