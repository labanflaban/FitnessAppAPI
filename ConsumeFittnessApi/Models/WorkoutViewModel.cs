using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace ConsumeFittnessApi.Models
{
    public class WorkoutViewModel
    {
        [TempData]
        public List<DayViewModel> Days { get; set; } = new List<DayViewModel>();
        public List<ExerciseViewModel> Exercise { get; set; }

        public WorkoutViewModel()
        {
            if (Days.Count == 0) // Check if days are already added
            {
                string[] weekdays = { "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday", "Sunday" };

                foreach (var dayName in weekdays)
                {
                    Days.Add(new DayViewModel { Name = dayName, Exercises = new List<ExerciseViewModel>() });
                }
            }

        }
    }
}