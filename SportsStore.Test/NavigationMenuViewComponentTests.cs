using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using SportsStore.Components;
using SportsStore.Interfaces;
using SportsStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SportsStore.Test
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void Can_Select_Categories()
        {
            var mock = new Mock<IStoreRepository>();
            var products = new List<Product>() 
            { 
                new Product() { Name = "P1", Category = "Cat2" },
                new Product() { Name = "P2", Category = "Cat3" },
                new Product() { Name = "P3", Category = "Cat1" },
                new Product() { Name = "P4", Category = "Cat2" },
            };
            mock.Setup(x => x.Products).Returns(products.AsQueryable<Product>);

            var component = new NavigationMenuViewComponent(mock.Object);

            var viewComponentResult = component.Invoke() as ViewViewComponentResult;
            var model = viewComponentResult.ViewData.Model;

            string[] result = ((IEnumerable<string>) model).ToArray();

            Assert.Equal("Cat1", result[0]);
            Assert.Equal("Cat2", result[1]);
            Assert.Equal("Cat3", result[2]);
            Assert.Equal(3, result.Length);
        }
    }
}
