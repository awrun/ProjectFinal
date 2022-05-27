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

    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }



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
        public int yoe { get; set; }



        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public EditModel()
        {
            trainername = "";
            gender = "";
            dob = DateTime.Now;
            mobilenumber = "";
            address = "";
            city = "";
            state = "";
            pincode = "";
            yoe =0;
            SuccessMessage = "";
            ErrorMessage = "";
            genders = GetGenders();
        }

        private List<SelectListItem> GetGenders()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Male", Value = "M" });
            selectItems.Add(new SelectListItem { Text = "Female", Value = "F" });
            selectItems.Add(new SelectListItem { Text = "Unspecified", Value = "U" });

            return selectItems;
        }
        public void OnGet(int id)
        {
            Id = id;
            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }
            var trainerDataaccess = new TrainerDataAccess();
            var tra = trainerDataaccess.GetTrainerById(id);
            if (tra != null)
            {

                trainername = tra.trainername;
                gender = tra.gender;
                dob = tra.dob;
                mobilenumber = tra.mobilenumber;
                address = tra.address;
                city = tra.city;
                state = tra.state;
                pincode = tra.pincode;
                yoe = tra.yoe;

            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }

        }

        public void OnPost()
        {
            genders = GetGenders();

            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please correct and Try again";
                return;
            }


            var trainerDataaccess = new TrainerDataAccess();
            var traToUpdate = new TrainerDataModel { trainerid = Id, trainername = trainername, gender = gender, dob = dob, mobilenumber = mobilenumber, address = address, city = city, state = state, pincode = pincode, yoe = yoe};
            var updatedTrainer = trainerDataaccess.Update(traToUpdate);
            if (updatedTrainer != null)
            {
                SuccessMessage = $"Trainer {updatedTrainer.trainerid} updated successfully";
            }
            else
            {
                ErrorMessage = $"Error! Updating Trainer {trainerDataaccess.ErrorMessage}";
            }
        }

    }
}
