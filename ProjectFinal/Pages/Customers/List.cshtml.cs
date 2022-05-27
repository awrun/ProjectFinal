using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;

namespace ProjectFinal.Pages.Customers
{
    public class ListModel : PageModel
    {
        public List<CustomerDataModel> Customers { get; set; }

        [BindProperty]
        public string SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public ListModel()
        {
            SearchText = "";
            ErrorMessage = "";
            SuccessMessage = "";
            Customers = new List<CustomerDataModel>();

        }

        public void OnGet()
        {
            var customerDataaccess = new CustomerDataAccess();
            Customers = customerDataaccess.GetAll();
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
            CustomerDataAccess customerDataaccess = new CustomerDataAccess();
            Customers = customerDataaccess.GetCustomerByName(SearchText);
            if (Customers != null)
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
            var customerDataAccess = new CustomerDataAccess();
            Customers = customerDataAccess.GetAll();
        }




    }
}

