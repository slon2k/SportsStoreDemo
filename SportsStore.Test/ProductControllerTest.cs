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
            mock.Setup(x => x.Products).Returns((new List<Product>() { 
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
    }
}
