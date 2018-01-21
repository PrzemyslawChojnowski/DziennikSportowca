using DziennikSportowca.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class RankingsViewModel
    {
        public string AlreadyLoggedUserId { get; set; }

        public string RankingName1 { get; set; }

        public int LoggedUserPosition1 { get; set; }

        public List<IGrouping<ApplicationUser, UserTraining>> Trainings1 { get; set; }

        public List<ApplicationUser> AllUsers1 { get; set; }

        public string RankingName2 { get; set; }
        
        public int LoggedUserPosition2 { get; set; }

        public List<CardioSportRankingElement> Trainings2 { get; set; }

        public List<ApplicationUser> AllUsers2 { get; set; }

        public string RankingName3 { get; set; }

        public int LoggedUserPosition3 { get; set; }

        public List<CardioSportRankingElement> Trainings3 { get; set; }

        public List<ApplicationUser> AllUsers3 { get; set; }

        public RankingsViewModel()
        {
            Trainings1 = new List<IGrouping<ApplicationUser, UserTraining>>();
            Trainings2 = new List<CardioSportRankingElement>();
            Trainings3 = new List<CardioSportRankingElement>();
            AllUsers1 = new List<ApplicationUser>();
            AllUsers2 = new List<ApplicationUser>();
            AllUsers3 = new List<ApplicationUser>();
        }
    }
}
