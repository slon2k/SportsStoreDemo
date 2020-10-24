using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Moq;
using SportsStore.Infrastructure;
using SportsStore.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SportsStore.Test
{
    public class PageLinkTagHelperTest
    {
        [Fact]
        public void Can_Gererate_Page_Links()
        {
            var urlHelper = new Mock<IUrlHelper>();
            urlHelper.SetupSequence(x => x.Action(It.IsAny<UrlActionContext>()))
                .Returns("/Test/Page1")
                .Returns("/Test/Page2")
                .Returns("/Test/Page3");

            var urlHelperFactory = new Mock<IUrlHelperFactory>();
            urlHelperFactory.Setup(f => f.GetUrlHelper(It.IsAny<ActionContext>())).Returns(urlHelper.Object);

            var helper = new PageLinkTagHelper(urlHelperFactory.Object) 
            {
                PageModel = new PagingInfo() { Page = 2, PageSize = 10, TotalItems = 25 },
                PageAction = "Test",
                PageClass = "class1"
            };

            var content = new Mock<TagHelperContent>();
            var context = new TagHelperContext(new TagHelperAttributeList(), new Dictionary<object, object>(), "");
            var output = new TagHelperOutput("div", new TagHelperAttributeList(), (cache, encoder) => Task.FromResult(content.Object)); 

            helper.Process(context, output);

            Assert.Equal(
                @"<div>"+
                @"<a class=""class1"" href=""/Test/Page1"">1</a>" +
                @"<a class=""class1"" href=""/Test/Page2"">2</a>" +
                @"<a class=""class1"" href=""/Test/Page3"">3</a>" +
                @"</div>",
                output.Content.GetContent());
        }
    }
}
