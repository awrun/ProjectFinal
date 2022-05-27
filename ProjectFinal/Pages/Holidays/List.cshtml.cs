using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;

namespace ProjectFinal.Pages.Holidays
{
    public class ListModel : PageModel
    {
        public List<HolidayDataModel> Holidays { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public ListModel()
        {
            SearchText = "";
            ErrorMessage = "";
            SuccessMessage = "";
            Holidays = new List<HolidayDataModel>();

        }

        public void OnGet()
        {
            var holidayDataaccess = new HolidayDataAccess();
            Holidays = holidayDataaccess.GetAll();
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
            HolidayDataAccess holidayDataaccess = new HolidayDataAccess();
            Holidays = holidayDataaccess.GetHolidayByName(SearchText);
            if (Holidays != null)
            {
                SuccessMessage = "Searched Holiday Data Found";
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
            var holidayDataAccess = new HolidayDataAccess();
            Holidays = holidayDataAccess.GetAll();
        }




    }
}

