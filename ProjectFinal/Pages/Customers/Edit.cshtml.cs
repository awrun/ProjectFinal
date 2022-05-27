using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.Pages.Customers
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }



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

        public EditModel()
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
            var customerDataaccess = new CustomerDataAccess();
            var cus = customerDataaccess.GetCustomerById(id);
            if (cus != null)
            {

                customername = cus.customername;
                gender = cus.gender;
                dob = cus.dob;
                mobilenumber = cus.mobilenumber;
                address = cus.address;
                city = cus.city;
                state = cus.state;
                pincode = cus.pincode;
               
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


            var customerDataaccess = new CustomerDataAccess();
            var cusToUpdate = new CustomerDataModel { customerid = Id, customername = customername, gender = gender, dob = dob, mobilenumber = mobilenumber, address = address, city = city, state = state, pincode = pincode };
            var updatedCustomer = customerDataaccess.Update(cusToUpdate);
            if (updatedCustomer != null)
            {
                SuccessMessage = $"Customer {updatedCustomer.customerid} updated successfully";
            }
            else
            {
                ErrorMessage = $"Error! Updating Student {customerDataaccess.ErrorMessage}";
            }
        }

    }
}
