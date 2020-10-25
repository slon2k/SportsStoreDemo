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
            var filteredProducts = repository.Products.Where(x => category == null || x.Category == category);
            var products = filteredProducts.Skip(pageSize * (page - 1)).Take(pageSize);
            var model = new ProductListViewModel()
            {
                Products = products,
                CurrentCategory = category,
                PagingInfo = new PagingInfo()
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalItems = filteredProducts.Count()
                }
            };

            return View(model);
        }
    }
}
