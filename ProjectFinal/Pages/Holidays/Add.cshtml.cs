using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.Pages.Holidays
{
    [Authorize(Roles = "Admin")]

    public class AddModel : PageModel
    {
        [BindProperty]
        [Display(Name = "description")]
        [Required]
        public string description { get; set; }


        [BindProperty]
        [Display(Name = "date")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime date { get; set; }

        

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public AddModel()
        {
            description = "";
            date = DateTime.Now;
        }


        public void OnGet()
        {
        }

       
        public void OnPost()
        {




            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data,Please Try Again";
                return;
            }


            var holidayDataaccess = new HolidayDataAccess();
            var newHoliday = new HolidayDataModel { description = description, date = date};
            var insertedHoliday = holidayDataaccess.Insert(newHoliday);
            if (insertedHoliday != null && insertedHoliday.holidayid > 0)
            {
                SuccessMessage = $"Successfully Inserted Holiday {insertedHoliday.holidayid}";
                ModelState.Clear();

            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please Try Again - {holidayDataaccess.ErrorMessage}";
            }


        }

    }
}
