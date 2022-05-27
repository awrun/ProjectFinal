using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;

namespace ProjectFinal.Pages.Activates
{
    public class ListModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public List<ActivateViewModel> Activates { get; set; }

        public void OnGet()
        {
            var activateDataaccess = new ActivateDataAccess();
            Activates = activateDataaccess.GetAll();
        }
    }
}
