using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace ProjectFinal.Pages.Activates
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }



        [BindProperty]
        [Display(Name = "customerid")]
        [Required]
        public int customerid { get; set; }



        [BindProperty]
        [Display(Name = "packageid")]
        [Required]
        public int packageid { get; set; }

        [BindProperty]
        [Display(Name = "startdate")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime startdate { get; set; }

        [BindProperty]
        [Display(Name = "enddate")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime enddate { get; set; }

        [BindProperty]
        [Display(Name = "status")]
        [Required]
        public string status { get; set; }
       



        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public EditModel()
        {
            customerid = 0;
            packageid = 0;
            startdate = DateTime.Now;
            enddate = DateTime.Now;
            status = "";
            SuccessMessage = "";
            ErrorMessage = "";
        }

       
        public void OnGet(int id)
        {
            Id = id;
            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }
            var activateDataaccess = new ActivateDataAccess();
            var act = activateDataaccess.GetActivateById(id);
            if (act != null)
            {

                customerid = act.customerid;
                packageid = act.packageid;
                startdate = act.startdate;
                enddate = act.enddate;
                status = act.status;
                

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


            var activateDataaccess = new ActivateDataAccess();
            var actToUpdate = new ActivateDataModel { activateid = Id, customerid = customerid, packageid = packageid, startdate = startdate, enddate = enddate, status = status };
            var updatedActivate = activateDataaccess.Update(actToUpdate);
            if (updatedActivate != null)
            {
                SuccessMessage = $"Activate {updatedActivate.activateid} updated successfully";
            }
            else
            {
                ErrorMessage = $"Error! Updating Activate {activateDataaccess.ErrorMessage}";
            }
        }

    }
}
