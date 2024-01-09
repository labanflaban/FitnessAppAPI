using ConsumeFittnessApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text;

namespace ConsumeFittnessApi.Controllers
{
    public class MuscleGroupController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7105/api");
        private readonly HttpClient _client;

        public MuscleGroupController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }
        [HttpGet]
        public IActionResult Index()
        {

            List<MuscleGroupViewModel> muscleGroupList = new List<MuscleGroupViewModel>();
            //api/MuscleGroups/GetMuscleGroups
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/MuscleGroups/GetMuscleGroups").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                muscleGroupList = JsonConvert.DeserializeObject<List<MuscleGroupViewModel>>(data);
            }

            return View(muscleGroupList);
        }
       

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(MuscleGroupViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                ///api/MuscleGroups/PostMuscleGroup
                ///
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "/MuscleGroups/PostMuscleGroup",
                    content).Result;
                if (response.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Muscle Group created.";
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
        public IActionResult Edit(int Id)
        {
            try
            {
                ////api/MuscleGroups/GetMuscleGroup/1
                MuscleGroupViewModel muscleGroup = new MuscleGroupViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/MuscleGroups/GetMuscleGroup/" + Id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    muscleGroup = JsonConvert.DeserializeObject<MuscleGroupViewModel>(data);
                }
                return View(muscleGroup);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }

        }

        [HttpPost]
        public IActionResult Save(MuscleGroupViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress +
                    "/MuscleGroups/PutMuscleGroup/" + model.Id, content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["errorMessage"] = "Muscle Groups details updated";
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
        public IActionResult Delete(int Id)
        {
            try
            {
                MuscleGroupViewModel muscleGroup = new MuscleGroupViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress +
                    "/MuscleGroups/GetMuscleGroup/" + Id).Result;
                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    muscleGroup = JsonConvert.DeserializeObject<MuscleGroupViewModel>(data);
                }
                return View(muscleGroup);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteConfirmed(int Id) 
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress
                        + "/MuscleGroups/DeleteMuscleGroup/" + Id).Result;

                if (response.IsSuccessStatusCode)
                {

                    TempData["successMessage"] = "Muscle Group deleted.";
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
        public IActionResult GetExercise()
        {

            List<ExerciseViewModel> exericiseList = new List<ExerciseViewModel>();
            //api/MuscleGroups/GetMuscleGroups
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/Exercise/GetExercise").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                exericiseList = JsonConvert.DeserializeObject<List<ExerciseViewModel>>(data);
            }

            return View(exericiseList);
        }
    }
}
