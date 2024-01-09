using ConsumeFittnessApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsumeFittnessApi.Controllers
{
    public class DayController : Controller
    {
        // GET: DayController
        public ActionResult Index()
        {
            DayViewModel viewModel = new DayViewModel();

            return View(viewModel);
        }
        
        public ActionResult AddExercise(DayViewModel model)
        {
            DayViewModel viewModel = new DayViewModel();

            return View();
        }
   
    }
}
