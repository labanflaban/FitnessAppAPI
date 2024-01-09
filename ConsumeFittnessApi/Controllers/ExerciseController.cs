using ConsumeFittnessApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;

namespace ConsumeFittnessApi.Controllers
{
    public class ExerciseController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7105/api");
        public readonly HttpClient _client;
        public ExerciseController() 
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        [HttpGet]
        public IActionResult Index()
        {

            List<ExerciseViewModel> ExerciseList = new List<ExerciseViewModel>();
            //api/MuscleGroups/GetMuscleGroups
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Exercises").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                ExerciseList = JsonConvert.DeserializeObject<List<ExerciseViewModel>>(data);
            }

            return View(ExerciseList);
        }
        [HttpGet]
        public IActionResult Delete(int Id)
        {
            try
            {
                ExerciseViewModel exercise = new ExerciseViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/Exercises/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    exercise = JsonConvert.DeserializeObject<ExerciseViewModel>(data);
                }
                return View(exercise);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress
                        + "/Exercises/" + Id).Result;

                if (response.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Exercise deleted.";
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

        [HttpGet]
        public IActionResult Create() 
        {
            return View();
        }
    }
}
