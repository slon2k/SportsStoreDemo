using Microsoft.AspNetCore.Mvc;
using Moq;
using SportsStore.Controllers;
using SportsStore.Interfaces;
using SportsStore.Models;
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

            var result = (homeController.Index() as ViewResult).ViewData.Model as IEnumerable<Product>;

            var products = result.ToArray();
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

            var result = (homeController.Index(page: 1, pageSize: 3) as ViewResult).ViewData.Model as IEnumerable<Product>;
            var products1 = result.ToArray();
            Assert.Equal(3, products1.Length);
            Assert.Equal("Product 1", products1[0].Name);
            Assert.Equal("Product 2", products1[1].Name);
            Assert.Equal("Product 3", products1[2].Name);

            result = (homeController.Index(page: 2, pageSize: 6) as ViewResult).ViewData.Model as IEnumerable<Product>;
            var products2 = result.ToArray();
            Assert.Equal(2, products2.Length);
            Assert.Equal("Product 7", products2[0].Name);
            Assert.Equal("Product 8", products2[1].Name);
        }
    }
}
