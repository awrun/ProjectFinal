using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ProjectFinal.DataAccess;
using ProjectFinal.Models;

namespace ProjectFinal.Pages
{
    public class IndexModel : PageModel
    {
        public int TrainerCount { get; set; }
        public int CustomerCount { get; set; }
       

        public string ErrorMessage { get; set; }

        [FromQuery(Name = "action")]
        public string Action { get; set; }
        private readonly ILogger<IndexModel> _logger;


        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            TrainerCount = 0;
            CustomerCount = 0;
            ErrorMessage = "";

        }
        public void OnGet()
        {
            if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
            {
                Logout();
                return;
            }

            var dashBoardDataAccess = new DashBoardDataAccess();
            var dashboard = dashBoardDataAccess.GetAll();
            if (dashboard != null)
            {
                TrainerCount = dashboard.TrainerCount;
                CustomerCount = dashboard.CustomerCount;
            }
            else
            {
                ErrorMessage = $"No Dashboard Data Available - {dashBoardDataAccess.ErrorMessage}";
            }
        }
        public void OnPost()
        {
            Logout();
        }
        private void Logout()
        {
            HttpContext.SignOutAsync();
            Response.Redirect("/Index");
        }
    }
}