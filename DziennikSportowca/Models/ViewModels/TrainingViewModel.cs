using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DziennikSportowca.Models.ViewModels
{
    public class TrainingViewModel
    {
        public TrainingViewModel()
        {
            RepsNo = new List<int>();
            SeriesNo = new List<int>();
        }

        public TrainingPlan TrainingPlan { get;set; }
        public List<int> RepsNo { get; set; }
        public List<int> SeriesNo { get; set; }
        public List<int> Weight { get; set; }
        public List<int> ExerciseLength { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
