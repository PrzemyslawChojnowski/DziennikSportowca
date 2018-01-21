using MimeKit.Encodings;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using DziennikSportowca.Data;
using DziennikSportowca.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DziennikSportowca.Models;
using Microsoft.AspNetCore.Identity;

namespace DziennikSportowca.Controllers
{
    public class RankingsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;

        public RankingsController(ApplicationDbContext context, UserManager<ApplicationUser> manager)
        {
            _context = context;
            _manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            List<ApplicationUser> users1 = _context.ApplicationUser.ToList();
            List<ApplicationUser> users2 = new List<ApplicationUser>(users1);
            List<ApplicationUser> users3 = new List<ApplicationUser>(users1);
            string loggedUser = await _manager.GetUserIdAsync(await _manager.GetUserAsync(User));

            var doneTrainingsRanking = await createDoneTrainingsRankingAsync();
            var cardioTotalTimeRanking = await createCardioTotalTimeRankingAsync();
            var groupSportsTotalTimeRanking = await createGroupSportsTotalTimeRankingAsync();

            RankingsViewModel model = new RankingsViewModel()
            {                
                RankingName1 = doneTrainingsRanking.Item1,
                Trainings1 = doneTrainingsRanking.Item2,
                AlreadyLoggedUserId = loggedUser,
                AllUsers1 = users1,
                RankingName2 = cardioTotalTimeRanking.Item1,
                Trainings2 = cardioTotalTimeRanking.Item2,
                AllUsers2 = users2,
                RankingName3 = groupSportsTotalTimeRanking.Item1,
                Trainings3 = groupSportsTotalTimeRanking.Item2,
                AllUsers3 = users3
            };

            return View(model);
        }

        private async Task<(string, List<IGrouping<ApplicationUser, UserTraining>>)> createDoneTrainingsRankingAsync()
        {             
            var doneTrainingsCount = await _context.UserTrainings.
                                            Include(x => x.Training).
                                                ThenInclude(x => x.User).
                                            GroupBy(x => x.Training.User).
                                            OrderByDescending(x => x.Count()).
                                            ToListAsync();

            return ("Wykonane treningi", doneTrainingsCount);
        }

        private async Task<(string, List<CardioSportRankingElement>)> createCardioTotalTimeRankingAsync()
        {
            var groupedCardioExercises = await _context.ExercisesWeights.
                                                    Include(x => x.UserTrainingExerciseResult).
                                                        ThenInclude(x => x.UserTraining).
                                                            ThenInclude(x => x.Training).
                                                                ThenInclude(x => x.User).
                                                    Include(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise).
                                                        ThenInclude(x => x.ActivityType).
                                                    Where(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.ActivityType.Description == "Ćwiczenia wytrzymałościowe").
                                                    GroupBy(x => x.UserTrainingExerciseResult.UserTraining.Training.User).
                                                    ToListAsync();

            var cardioExercisesTotalTime = groupedCardioExercises.Select(x => 
                                            new CardioSportRankingElement
                                            {
                                                Key = x.Key,
                                                Sum = x.Sum(z => z.Result)
                                            }).
                                            OrderByDescending(x => x.Sum).
                                            ToList();

            return ("Łączny czas ćwiczeń cardio", cardioExercisesTotalTime);
        }

        private async Task<(string, List<CardioSportRankingElement>)> createGroupSportsTotalTimeRankingAsync()
        {
            var groupedGroupSportsExercises = await _context.ExercisesWeights.
                                                    Include(x => x.UserTrainingExerciseResult).
                                                        ThenInclude(x => x.UserTraining).
                                                            ThenInclude(x => x.Training).
                                                                ThenInclude(x => x.User).
                                                    Include(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise).
                                                        ThenInclude(x => x.ActivityType).
                                                    Where(x => x.UserTrainingExerciseResult.TrainingPlanExercise.Exercise.ActivityType.Description == "Sporty grupowe").
                                                    GroupBy(x => x.UserTrainingExerciseResult.UserTraining.Training.User).
                                                    ToListAsync();

            var groupSportsExercisesTotalTime = groupedGroupSportsExercises.Select(x =>
                                            new CardioSportRankingElement
                                            {
                                                Key = x.Key,
                                                Sum = x.Sum(z => z.Result)
                                            }).
                                            OrderByDescending(x => x.Sum).
                                            ToList();

            return ("Łączny czas sportów grupowych", groupSportsExercisesTotalTime);
        }
    }

    public class CardioSportRankingElement
    {
        public ApplicationUser Key { get; set; }
        public double Sum { get; set; }
    }
}