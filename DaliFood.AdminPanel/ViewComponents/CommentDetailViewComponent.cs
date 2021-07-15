using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaliFood.AdminPanel.ViewComponents
{
    public class CommentDetailViewComponent: ViewComponent
    {
        readonly UnitOfWork unitOfWork;
        public CommentDetailViewComponent(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IViewComponentResult> InvokeAsync(int CommentId)
        {
            var comment= unitOfWork.CustomerCommentRepository.GetById(CommentId);
            comment.Customer = unitOfWork.CustomerRepository.GetById(comment.CustomerId);
            return View("/Pages/Shared/Components/_CommentDetail.cshtml", comment);
        }
    }
}
