using ACE_App.Models;
using ACE_App.Services;
using Microsoft.AspNetCore.Mvc;

namespace ACE_App.Controllers
{
    public class ACEController : Controller
    {
        private readonly IACEService _ace;
        public ACEController(IACEService ace)
        {
            _ace = ace;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ACEModel item)
        {
            var result = await _ace.GetEstimation(item.UserInput);
            Thread.Sleep(5000); // to immitate a delayed response from API
            item.UserInput = "";

            if (!string.IsNullOrEmpty(result.MLOutput))
            {
                item.MLOutput = result.MLOutput;    
            }
            item.ErrorMessage = result.ErrorMessage;
            if (string.IsNullOrEmpty(item.ErrorMessage))
                ViewBag.DisplayErrorMessage = false;
            else
                ViewBag.DisplayErrorMessage = true;
            ViewBag.EnableSubmit = true;
            return View("Index", item);
        }
    }
}
