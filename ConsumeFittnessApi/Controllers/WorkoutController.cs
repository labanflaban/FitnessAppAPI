using ConsumeFittnessApi.Models;
using ConsumeFittnessApi.SessionHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;

namespace ConsumeFittnessApi.Controllers
{
    public class WorkoutController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7105/api");

        private readonly HttpClient _client;
        private readonly WorkoutViewModel _viewModel;

        public WorkoutController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
            _viewModel = new WorkoutViewModel();

        }
        // GET: WorkoutController
        public ActionResult Index()
        {
            var exercises = GetExercises();
            var viewModel = new WorkoutViewModel
            {
                Exercise = exercises
            };
            return View(viewModel);
        }


        private List<ExerciseViewModel> GetExercises()
        {
            //https://localhost:7105/api/Exercises
            List<ExerciseViewModel> exerciseList = new List<ExerciseViewModel>();
            //api/MuscleGroups/GetMuscleGroups
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Exercises").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                exerciseList = JsonConvert.DeserializeObject<List<ExerciseViewModel>>(data);
            }

            return exerciseList;
        }
        private List<MuscleGroupViewModel> GetMuscleGroups()
        {
            List<MuscleGroupViewModel> muscleGroupList = new List<MuscleGroupViewModel>();
            //api/MuscleGroups/GetMuscleGroups
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/MuscleGroups/GetMuscleGroups").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                muscleGroupList = JsonConvert.DeserializeObject<List<MuscleGroupViewModel>>(data);
            }

            return muscleGroupList;
        }


        public IActionResult AddExercise(WorkoutViewModel model, string dayName, string exerciseName)
        {
            // Check if Exercise list is null or empty
            if (model.Exercise == null || !model.Exercise.Any())
            {
                model.Exercise = GetExercises();
            }

            var selectedDay = model.Days.FirstOrDefault(d => d.Name == dayName);

            if (selectedDay != null)
            {
                if (selectedDay.Exercises == null)
                {
                    selectedDay.Exercises = new List<ExerciseViewModel>();
                }

                if (!string.IsNullOrEmpty(exerciseName))
                {
                    selectedDay.Exercises.Add(new ExerciseViewModel { Name = exerciseName });
                }
            }

            return View("Index", model);
        }
    }
}
