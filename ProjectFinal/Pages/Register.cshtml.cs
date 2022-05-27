using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.Pages
{
   
    public class RegisterModel : PageModel
    {
        [BindProperty]
        [Display(Name = "customername")]
        [Required]
        public string customername { get; set; }



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



        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public RegisterModel()
        {
            customername = "";
            gender = "";
            dob = DateTime.Now;
            mobilenumber = "";
            address = "";
            city = "";
            state = "";
            pincode = "";
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


            var customerDataaccess = new CustomerDataAccess();
            var newCustomer = new CustomerDataModel { customername = customername, gender = gender, dob = dob, mobilenumber = mobilenumber, address = address, city = city, state = state, pincode = pincode };
            var insertedCustomer = customerDataaccess.Insert(newCustomer);
            if (insertedCustomer != null && insertedCustomer.customerid > 0)
            {
                SuccessMessage = $"Successfully Registered ";
                ModelState.Clear();

            }
            else
            {
                ErrorMessage = $"Error! Add Failed.Please Try Again - {customerDataaccess.ErrorMessage}";
            }


        }

    }
}
