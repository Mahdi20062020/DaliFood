using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public void OnGet(int? SearchStatus,int? CustomerId, string SearchQ = null, string SearchStartDate = null, string SearchEndDate = null)
        {
            ViewData["CustomerId"] = 
                new SelectList(unitofwork.CustomerRepository.GetAll(), "Id", "Name");

            var customerId = User.Claims.Where(p => p.Type == SD.CustomerId).FirstOrDefault().Value;
            
            if (customerId == SD.AdminCustomerId)
            {
                CustomerComment = unitofwork.CustomerCommentRepository
                .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
                if (CustomerId.HasValue)
                {
                    CustomerComment = unitofwork.CustomerCommentRepository
                   .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate), where: p => p.CustomerId == CustomerId);
                }
            }
            else
            {
                CustomerComment = unitofwork.CustomerCommentRepository
               .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate),where:p=>p.CustomerId == int.Parse(customerId));
            }
            if (SearchStatus.HasValue)
            {
                CustomerComment = CustomerComment.Where(p => p.Status == SearchStatus);
            }     
            if (SearchQ != null)
            {
                foreach (var item in SearchQ.ToLower().Split(' '))
                {
                    CustomerComment = CustomerComment.Where(p => p.Name.ToLower().Contains(item)|| p.Family.ToLower().Contains(item) || p.Text.ToLower().Contains(item) || p.Customer.Name.ToLower().Contains(item));
                }
            }
            if (SearchStartDate != null)
            {
                var date = SearchStartDate.Split('/');
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                var Startdate = new DateTime(year, month, day);
                CustomerComment = CustomerComment.Where(p => p.CreateDate >= Startdate);
            }
            if (SearchEndDate != null)
            {
                var date = SearchEndDate.Split('/');
                int year = int.Parse(date[0]);
                int month = int.Parse(date[1]);
                int day = int.Parse(date[2]);
                var Startdate = new DateTime(year, month, day);
                CustomerComment = CustomerComment.Where(p => p.CreateDate <= Startdate);
            }
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
