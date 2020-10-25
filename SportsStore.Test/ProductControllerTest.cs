using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Interfaces;
using SportsStore.Models;
using SportsStore.ViewModels;
using System.Collections.Generic;
using System.Linq;
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

            var result = (homeController.Index(category: null) as ViewResult).ViewData.Model as ProductListViewModel;

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

            var result = (homeController.Index(category: null, page: 1, pageSize: 3) as ViewResult).ViewData.Model as ProductListViewModel;
            var products1 = result.Products.ToArray();
            Assert.Equal(3, products1.Length);
            Assert.Equal("Product 1", products1[0].Name);
            Assert.Equal("Product 2", products1[1].Name);
            Assert.Equal("Product 3", products1[2].Name);
            Assert.Equal(8, result.PagingInfo.TotalItems);
            Assert.Equal(3, result.PagingInfo.TotalPages);
            Assert.Equal(3, result.PagingInfo.PageSize);
            Assert.Equal(1, result.PagingInfo.Page);

            result = (homeController.Index(category: null, page: 2, pageSize: 6) as ViewResult).ViewData.Model as ProductListViewModel;
            var products2 = result.Products.ToArray();
            Assert.Equal(2, products2.Length);
            Assert.Equal("Product 7", products2[0].Name);
            Assert.Equal("Product 8", products2[1].Name);
            Assert.Equal(8, result.PagingInfo.TotalItems);
            Assert.Equal(2, result.PagingInfo.TotalPages);
            Assert.Equal(6, result.PagingInfo.PageSize);
            Assert.Equal(2, result.PagingInfo.Page);
        }

        [Fact]
        public void Can_Filter_Category()
        {
            var mock = new Mock<IStoreRepository>();
            var products = new List<Product>()
            {
                new Product(){ Name = "P1", Category = "Cat1" },
                new Product(){ Name = "P2", Category = "Cat1" },
                new Product(){ Name = "P3", Category = "Cat1" },
                new Product(){ Name = "P4", Category = "Cat1" },
                new Product(){ Name = "P5", Category = "Cat2" },
                new Product(){ Name = "P6", Category = "Cat2" },
            };

            mock.Setup(x => x.Products).Returns(products.AsQueryable<Product>);

            var homeController = new HomeController(mock.Object);

            var result1 = (homeController.Index(category: "Cat1") as ViewResult).ViewData.Model as ProductListViewModel;
            var result2 = (homeController.Index(category: "Cat2") as ViewResult).ViewData.Model as ProductListViewModel;

            var products1 = result1.Products.ToList();
            var products2 = result2.Products.ToList();

            Assert.Equal(4, products1.Count());
            Assert.Equal(2, products2.Count());

            Assert.Equal("P1", products1[0].Name);
            Assert.Equal("P2", products1[1].Name);
            Assert.Equal("P3", products1[2].Name);
            Assert.Equal("P4", products1[3].Name);

            Assert.Equal("P5", products2[0].Name);
            Assert.Equal("P6", products2[1].Name);
        }
    }
}
