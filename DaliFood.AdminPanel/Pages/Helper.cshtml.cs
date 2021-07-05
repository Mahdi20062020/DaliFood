using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DaliFood_AdminPanelML.Model;
using DaliFood.Utilites;

namespace DaliFood.AdminPanel.Pages
{
    public class HelperModel : PageModel
    {
        public ActionResult OnGet()
        {
            return Redirect("/");
        }
        public ActionResult OnPost(string Request)
        {
            // Add input data
            var input = new ModelInput();
            input.Col1 = Request;
            // Load model and predict output of sample data
            ModelOutput result = ConsumeModel.Predict(input);
            if (Utilites.Utilites.DoHelper(result)=="404")
            {
                return NotFound();
            }
            return Redirect(Utilites.Utilites.DoHelper(result));
        }
    }
}
