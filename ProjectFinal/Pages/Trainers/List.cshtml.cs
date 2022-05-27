using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;

namespace ProjectFinal.Pages.Trainers
{
    public class ListModel : PageModel
    {
        public List<TrainerDataModel> Trainers { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public ListModel()
        {
            SearchText = "";
            ErrorMessage = "";
            SuccessMessage = "";
            Trainers = new List<TrainerDataModel>();

        }

        public void OnGet()
        {
            var trainerDataaccess = new TrainerDataAccess();
            Trainers = trainerDataaccess.GetAll();
        }
        public void OnPostSearch()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }
            if (string.IsNullOrEmpty(SearchText) || SearchText.Length < 3)
            {
                ErrorMessage = "Please Input More Than 1 Character";
                return;
            }
            TrainerDataAccess trainerDataaccess = new TrainerDataAccess();
            Trainers = trainerDataaccess.GetTrainerByName(SearchText);
            if (Trainers != null)
            {
                SuccessMessage = "Searched customer Data Found";
                return;
            }
            else
            {
                ErrorMessage = "Error";
            }
        }
        public void OnPostClear()
        {

            SearchText = "";
            ModelState.Clear();
            var trainerDataAccess = new TrainerDataAccess();
            Trainers = trainerDataAccess.GetAll();
        }




    }
}

