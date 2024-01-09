namespace ConsumeFittnessApi.Models
{
    public class DayViewModel
    {
        public string Name { get; set; }
        public List<ExerciseViewModel> Exercises { get; set; }

        public DayViewModel()
        {
            Exercises = new List<ExerciseViewModel>();
        }
    }
}
