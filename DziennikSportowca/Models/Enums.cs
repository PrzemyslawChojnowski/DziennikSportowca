using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DziennikSportowca.Models
{
    public enum Gender
    {
        [Display(Name = "Kobieta")]
        Woman = 1,

        [Display(Name = "Mężczyzna")]
        Man = 2
    }

    public enum GoalScope
    {
        [Display(Name = "Budowa ciała")]
        Physique = 1,

        [Display(Name = "Osiągnięcia w ćwiczeniach")]
        ExercisesGoals
    }

    public enum PhysiqueScope
    {
        [Display(Name = "Zrzucić wagę")]
        LoseWeight = 1,

        [Display(Name = "Przybrać na wadze")]
        GainWeight,

        [Display(Name = "Zmniejszyć Body Fat")]
        ReduceBF,

        [Display(Name = "Zwiększyć Body Fat")]
        IncreaseBF,

        [Display(Name = "Zmniejszyć rozmiar części ciała")]
        DecreaseCircumference,

        [Display(Name = "Zwiększyć rozmiar części ciała")]
        IncreaseCircumference
    }

    public enum ExerciseScope
    {
        [Display(Name = "Pokonać określony dystans \"na raz\"")]
        OvercomeTheSpecifiedDistanceAtOnce = 1,

        [Display(Name = "Pokonać określony dystans \"w sumie\"")]
        OvercomeTheSpecifiedDistanceAtAll,

        [Display(Name = "Wykonywać ćwiczenia wytrzymałościowe przez określony czas \"na raz\"")]
        DoCardioExercisesForASpecifiedTimeAtOnce,

        [Display(Name = "Wykonywać ćwiczenia wytrzymałościowe przez określony czas \"w sumie\"")]
        DoCardioExercisesForASpecifiedTimeAtAll,

        [Display(Name = "Podnieść jednorazowo określony ciężar")]
        PickUpTheSpecifiedWeight
    }

    public enum Circumferences
    {
        [Display(Name = "Obwód w barkach")]
        ShouldersCircumference = 1,

        [Display(Name = "Obwód klatki piersiowej")]
        ChestCircumference,

        [Display(Name = "Obwód talii")]
        WaistCircumference,

        [Display(Name = "Obwód bicepsa")]
        BicepsCircumference,

        [Display(Name = "Obwód tricepsa")]
        TricepsCircumference,

        [Display(Name = "Obwód uda")]
        ThighCircumference,

        [Display(Name = "Obwód bioder")]
        HipCircumference
    }

    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
