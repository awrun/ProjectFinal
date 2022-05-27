using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.Pages.Trainers
{
    [Authorize(Roles = "Admin")]

    public class AddModel : PageModel
    {
        [BindProperty]
        [Display(Name = "trainername")]
        [Required]
        public string trainername { get; set; }



        [BindProperty]
        [Display(Name = "gender")]
        [Required]
        public string gender { get; set; }
        public List<SelectListItem> genders { get; set; }

        [BindProperty]
        [Display(Name = "dob")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime dob { get; set; }

        [BindProperty]
        [Display(Name = "mobilenumber")]
        [Required]
        public string mobilenumber { get; set; }
        [BindProperty]
        [Display(Name = "address")]
        [Required]
        public string address { get; set; }

        [BindProperty]
        [Display(Name = "city")]
        [Required]
        public string city { get; set; }

        [BindProperty]
        [Display(Name = "state")]
        public string state { get; set; }


        [BindProperty]
        [Display(Name = "pincode")]
        public string pincode { get; set; }

        [BindProperty]
        [Display(Name = "yoe")]
        [Required]
        public int yoe { get; set; }



        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public AddModel()
        {
            trainername = "";
            gender = "";
            dob = DateTime.Now;
            mobilenumber = "";
            address = "";
            city = "";
            state = "";
            pincode = "";
            yoe = 0;
            SuccessMessage = "";
            ErrorMessage = "";
            genders = GetGenders();
        }


        public void OnGet()
        {
        }

        private List<SelectListItem> GetGenders()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Male", Value = "M" });
            selectItems.Add(new SelectListItem { Text = "Female", Value = "F" });
            selectItems.Add(new SelectListItem { Text = "Unspecified", Value = "U" });

            return selectItems;
        }
        public void OnPost()
        {

            genders = GetGenders();



            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid data,Please Try Again";
                return;
            }


            var trainerDataaccess = new TrainerDataAccess();
            var newTrainer = new TrainerDataModel { trainername = trainername, gender = gender, dob = dob, mobilenumber = mobilenumber, address = address, city = city, state = state, pincode = pincode, yoe = yoe};
            var insertedTrainer = trainerDataaccess.Insert(newTrainer);
            if (insertedTrainer != null && insertedTrainer.trainerid > 0)
            {
                SuccessMessage = $"Successfully Inserted Customer {insertedTrainer.trainerid}";
                ModelState.Clear();

            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please Try Again - {trainerDataaccess.ErrorMessage}";
            }


        }

    }
}
