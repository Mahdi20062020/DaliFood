using DaliFood.WebApi.ViewModels;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using DaliFood.Models.Identity;

namespace DaliFood.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CommentController : Controller
    {
        readonly UnitOfWork unitofwork;
        readonly UserManager<ApplicationUser> _userManager;
        public CommentController(UnitOfWork unitofwork, UserManager<ApplicationUser> userManager)
        {
            this.unitofwork = unitofwork;
            _userManager = userManager;
        }
        /// <summary>دریافت تمام کامنت های موجود
        /// </summary>
        /// <param name="ItemPerPage">تعداد آیتم های نمایشی</param>
        /// <param name="PageNum">صفحه مورد نمایش</param>
        /// <param name="CustomerId">شناسه فورشگاهی که قرار است کامنت های آن نمایش داده شود</param>
        [HttpGet("/api/Customer/{CustomerId}/Comments")]
        public IActionResult GetCustomerComments(int? ItemPerPage, int? PageNum, int CustomerId)
        { 
           
            var CustomerComments = unitofwork.CustomerCommentRepository.GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            List<CustomerComment> ItemsforShow = new();
            foreach (var item in CustomerComments)
            {
                item.Customer = unitofwork.CustomerRepository.GetById(item.CustomerId);
                CustomerComment itemToViewModel = item;
                ItemsforShow.Add(itemToViewModel);
            }
            IEnumerable<CustomerComment> result = ItemsforShow;
            result = result.Where(p => p.CustomerId == CustomerId);

            if (ItemPerPage.HasValue && PageNum.HasValue)
            {
                var Skipcount = (PageNum.Value - 1) * ItemPerPage.Value;
                result = result.Skip(Skipcount);
                result = result.Take(ItemPerPage.Value);

                if (result == null)
                {
                    return NotFound();
                }
            }
            return Ok(result);
        }
        /// <summary>افزودن کامنت (در صورت لاگین نبودن کاربر، نظر به عنوان مهمان ثبت خواهد شد)
        /// </summary>
        /// <param name="model">داده های کامنت</param>
        [HttpPost("Add")]
        [AllowAnonymous]
        public IActionResult PostCustomerComment(CustomerComment model)
        {
            if (ModelState.IsValid)
            {
                Models.CustomerComment customerComment = new()
                {
                    CreateDate = DateTime.Now,
                    Text = model.Text,
                    Status = 0,
                    Customer = unitofwork.CustomerRepository.GetById(model.CustomerId),
                    CustomerId=model.CustomerId ,
                    Name= model.Name,
                    Family=model.Family                
                };
                var user = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier);
                if (user.Any())
                {
                    var userId = user.FirstOrDefault().Value;
                    var CurrentUser = _userManager.Users.Where(p=>p.Id==userId).FirstOrDefault();
                    customerComment.UserId = userId;
                    customerComment.Name = CurrentUser.Name;
                    customerComment.Family = CurrentUser.Family;
                }
                if(unitofwork.CustomerCommentRepository.Create(customerComment))
                {
                    if(unitofwork.CustomerCommentRepository.Save())
                    {
                        return Ok();
                    }
                }
                ModelState.AddModelError("400", "Somthing is wrong");
            }
            return BadRequest(ModelState);

        }
        /// <summary>ویرایش کامنت(تنها افرادی که لاگین شده اند میتوانند نظرشان را تغییر دهند)
        /// </summary>
        /// <param name="model">داده های کامنت</param>
        /// <param name="id">آیدی کامنت مورد تغییر</param>
        [HttpPut("Edit")]
        public IActionResult PutCustomerComment(CustomerComment model,int id)
        {
            if (ModelState.IsValid)
            {
                var CustomerComment = unitofwork.CustomerCommentRepository.GetById(id);
                var user = User.Claims.Where(p => p.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
                if (CustomerComment.UserId!=user.Value)
                {
                    ModelState.AddModelError("403", "Not Authorized");
                    return BadRequest(ModelState);
                }
                if (!string.IsNullOrWhiteSpace(model.Text))
                {
                    CustomerComment.Text = model.Text;
                }
                if (unitofwork.CustomerRepository.GetAll(p=>p.Id==model.CustomerId).Any())
                {
                    CustomerComment.CustomerId = model.CustomerId;
                }

                if (unitofwork.CustomerCommentRepository.Modifie(CustomerComment))
                {
                    if (unitofwork.CustomerCommentRepository.Save())
                    {
                        return Ok();
                    }
                }
                ModelState.AddModelError("400", "Somthing is wrong");
            }
            return BadRequest(ModelState);
        }
    }
}
