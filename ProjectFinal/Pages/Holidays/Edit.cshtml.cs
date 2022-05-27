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

    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }



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

        public EditModel()
        {
            description = "";
            date = DateTime.Now;
        
        }

       
        public void OnGet(int id)
        {
            Id = id;
            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }
            var holidayDataaccess = new HolidayDataAccess();
            var hol = holidayDataaccess.GetHolidayById(id);
            if (hol != null)
            {

                description = hol.description;
                date = hol.date;

            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }

        }

        public void OnPost()
        {

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please correct and Try again";
                return;
            }


            var holidayDataaccess = new HolidayDataAccess();
            var holToUpdate = new HolidayDataModel { holidayid = Id, description = description, date = date};
            var updatedHoliday = holidayDataaccess.Update(holToUpdate);
            if (updatedHoliday != null)
            {
                SuccessMessage = $"Customer {updatedHoliday.holidayid} updated successfully";
            }
            else
            {
                ErrorMessage = $"Error! Updating Student {holidayDataaccess.ErrorMessage}";
            }
        }

    }
}
