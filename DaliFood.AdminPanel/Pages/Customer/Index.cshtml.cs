using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DaliFood.Utilites;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DaliFood.AdminPanel.Pages.Customer
{
    public class IndexModel : PageModel
    {
        readonly UnitOfWork unitofwork;
        public IndexModel(UnitOfWork unitofwork)
        {
            this.unitofwork = unitofwork;
        }



        public class GridModel {

            public string Name { get; set; }
            public string OwnerName { get; set; }
            public string CityName { get; set; }
            public string Status { get; set; }
            public string Type { get; set; }

        };
        
        [BindProperty]
        public IEnumerable<GridModel> grid { get; set; }

        public IEnumerable<Models.Customer> Customer { get; set; }


        public void OnGet()
        {
            Customer = unitofwork.CustomerRepository
                .GetAll(orderby: p => p.OrderByDescending(p => p.CreateDate));
            foreach (var item in Customer)
            {
                item.CustomerType = unitofwork
                    .CustomerTypeRepository.GetById(item.TypeId);
            }

            foreach (var item in Customer)
            {
                var model = new GridModel();
                model.Name = item.Name;
                model.OwnerName = item.Name;



            }




        }



    }
}
