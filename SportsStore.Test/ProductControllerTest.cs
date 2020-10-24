using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Interfaces;
using SportsStore.Models;
using SportsStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Test
{
    public class ProductControllerTest
    {
        [Fact]
        public void Can_Use_Repository()
        {
            var mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns((new List<Product>() 
            { 
                new Product(){ Name= "Product 1" },
                new Product(){ Name= "Product 2" },
            }).AsQueryable<Product>);

            var homeController = new HomeController(mock.Object);

            var result = (homeController.Index() as ViewResult).ViewData.Model as ProductListViewModel;

            var products = result.Products.ToArray();
            Assert.Equal(2, products.Length);
            Assert.Equal("Product 1", products[0].Name);
            Assert.Equal("Product 2", products[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            var mock = new Mock<IStoreRepository>();
            mock.Setup(x => x.Products).Returns((new List<Product>() 
            {
                new Product(){ Name= "Product 1" },
                new Product(){ Name= "Product 2" },
                new Product(){ Name= "Product 3" },
                new Product(){ Name= "Product 4" },
                new Product(){ Name= "Product 5" },
                new Product(){ Name= "Product 6" },
                new Product(){ Name= "Product 7" },
                new Product(){ Name= "Product 8" },
            }).AsQueryable<Product>);

            var homeController = new HomeController(mock.Object);

            var result = (homeController.Index(page: 1, pageSize: 3) as ViewResult).ViewData.Model as ProductListViewModel;
            var products1 = result.Products.ToArray();
            Assert.Equal(3, products1.Length);
            Assert.Equal("Product 1", products1[0].Name);
            Assert.Equal("Product 2", products1[1].Name);
            Assert.Equal("Product 3", products1[2].Name);
            Assert.Equal(8, result.PagingInfo.TotalItems);
            Assert.Equal(3, result.PagingInfo.TotalPages);
            Assert.Equal(3, result.PagingInfo.PageSize);
            Assert.Equal(1, result.PagingInfo.Page);

            result = (homeController.Index(page: 2, pageSize: 6) as ViewResult).ViewData.Model as ProductListViewModel;
            var products2 = result.Products.ToArray();
            Assert.Equal(2, products2.Length);
            Assert.Equal("Product 7", products2[0].Name);
            Assert.Equal("Product 8", products2[1].Name);
            Assert.Equal(8, result.PagingInfo.TotalItems);
            Assert.Equal(2, result.PagingInfo.TotalPages);
            Assert.Equal(6, result.PagingInfo.PageSize);
            Assert.Equal(2, result.PagingInfo.Page);
        }
    }
}
