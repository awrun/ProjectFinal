using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;

namespace ProjectFinal.Pages.Trainings
{
    public class ListModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public List<TrainingViewModel> Trainings { get; set; }

        public void OnGet()
        {
            var trainingDataaccess = new TrainingDataAccess();
            Trainings = trainingDataaccess.GetAll();
        }
    }
}
