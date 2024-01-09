using ConsumeFittnessApi.Models;
using FittnessAppAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeFittnessApi.Controllers
{
    public class SettingsController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:7105/api");
        private readonly HttpClient _client;

        public SettingsController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        // GET: SettingsController
        public ActionResult Index()
        {
            var muscleGroups = GetMuscleGroups();
            var exercises = GetExercises();

            var viewModel = new SettingsViewModel
            {
                Groups = muscleGroups,
                Exercises = exercises
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
        [HttpPost]
        public IActionResult AddMuscleGroup(MuscleGroupViewModel model)
        {   
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/MuscleGroups/PostMuscleGroup",
                    content).Result;
                if (response.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Muscle Group added successfully.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }


            return View();
        }
        [HttpPost]
        public IActionResult AddExercise(ExerciseViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                ///api/MuscleGroups/PostMuscleGroup
                ///
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/Exercises",
                    content).Result;
                if (response.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Exercise added successfully.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();
            }


            return View();
        }
    }
}
