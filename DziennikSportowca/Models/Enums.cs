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
        ExercisesGoals = 2
    }

    public enum PhysiqueScope
    {
        [Display(Name = "Zrzucić wagę")]
        LoseWeight = 1,

        [Display(Name = "Przybrać na wadze")]
        GainWeight = 2,

        [Display(Name = "Zmniejszyć Body Fat")]
        ReduceBF = 3,

        [Display(Name = "Zwiększyć Body Fat")]
        IncreaseBF = 4,

        [Display(Name = "Zmniejszyć rozmiar części ciała")]
        DecreaseCircumference = 5,

        [Display(Name = "Zwiększyć rozmiar części ciała")]
        IncreaseCircumference = 6
    }

    public enum ExerciseScope
    {
        [Display(Name = "Wykonywać sporty grupowe przez określony czas \"na raz\"")]
        DoGroupSportsForASpecifiedTimeAtOnce = 1,

        [Display(Name = "Wykonywać sporty grupowe przez określony czas \"w sumie\"")]
        DoGroupSportsForASpecifiedTimeAtAll = 2,

        [Display(Name = "Wykonywać ćwiczenia wytrzymałościowe przez określony czas \"na raz\"")]
        DoCardioExercisesForASpecifiedTimeAtOnce = 3,

        [Display(Name = "Wykonywać ćwiczenia wytrzymałościowe przez określony czas \"w sumie\"")]
        DoCardioExercisesForASpecifiedTimeAtAll = 4,

        [Display(Name = "Podnieść jednorazowo określony ciężar")]
        PickUpTheSpecifiedWeight = 5
    }

    public enum Circumferences
    {
        [Display(Name = "Obwód w barkach")]
        ShouldersCircumference = 1,

        [Display(Name = "Obwód klatki piersiowej")]
        ChestCircumference = 2,

        [Display(Name = "Obwód talii")]
        WaistCircumference = 3,

        [Display(Name = "Obwód bicepsa")]
        BicepsCircumference = 4,

        [Display(Name = "Obwód tricepsa")]
        TricepsCircumference = 5,

        [Display(Name = "Obwód uda")]
        ThighCircumference = 6,

        [Display(Name = "Obwód bioder")]
        HipCircumference = 7
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
