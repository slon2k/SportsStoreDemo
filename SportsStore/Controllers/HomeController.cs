using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Interfaces;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStoreRepository repository;

        public HomeController(IStoreRepository repository)
        {
            this.repository = repository;
        }

        public IActionResult Index(string category, int page=1, int pageSize = 4)
        {
            var model = new ProductListViewModel()
            {
                Products = repository.Products.Where(x => category == null || x.Category == category).Skip(pageSize * (page - 1)).Take(pageSize),
                PagingInfo = new PagingInfo()
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalItems = repository.Products.Count()
                }
            };

            return View(model);
        }
    }
}
