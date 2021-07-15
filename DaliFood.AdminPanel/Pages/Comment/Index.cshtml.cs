using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Comment
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }
        [BindProperty]
        public IEnumerable<Models.CustomerComment> CustomerComment { get; set; }

        public void OnGet()
        {
            CustomerComment = unitofwork.CustomerCommentRepository
                .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
        }
        public ActionResult OnPostStatus(int Id,int Status)
        {
            var CustomerComment = unitofwork.CustomerCommentRepository.GetById(Id);
            CustomerComment.Status = Status;
            if (unitofwork.CustomerCommentRepository.Modifie(CustomerComment))
            {
                if (unitofwork.CustomerCommentRepository.Save())
                {
                    return RedirectToPage("Index");
                }
            }
            return BadRequest();
        }
    }
}
