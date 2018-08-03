using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using TestApp.Core.Entity.Domain;
using TestApp.Infrastructure.Data;
using TestApp.Infrastructure.Data.Common;
using TestApp.Service.Domain;
using TestApp.Web.Controllers.Api;
using Xunit;

namespace TestApp.Web.Test
{
    public class ProductControllerTest
    {
        private readonly TestServer _server;
        public ProductControllerTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
        }

        [Fact]
        public async Task ReturnHomePageViewResult()
        {
            var client = _server.CreateClient();
            // Act
            var response = await client.GetAsync("http://localhost:65155/");
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.NotEmpty(responseString);
        }

        [Fact]
        public async Task Get_ReturnsAProductList()
        {
            // Arrange
            var mockDbContextOptions = new DbContextOptions<AppDbContext>();

            var mockDataContext = new Mock<AppDbContext>(mockDbContextOptions);
            var mockUnitOfWork = new Mock<UnitOfWork>(mockDataContext.Object);

            var mockProductStoreService = new Mock<ProductStoreService>(mockUnitOfWork.Object);
            var category = new Category();
            mockProductStoreService.Setup(service => service.GetProductsByCategory(category)).Returns(await Task.FromResult(GetTestProducts()));

            var controller = new ProductController(mockProductStoreService.Object){ 
                ControllerContext = new ControllerContext(new ActionContext() { HttpContext = new DefaultHttpContext() }) 
            };

            // Act
            var result = controller.Get(category.Id);

            // Assert
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<IEnumerable<Product>>(result);
        }


        private IEnumerable<Product> GetTestProducts()
        {
            var list = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Test Product One"
                },
                new Product
                {
                    Id = 2,
                    Name = "Test Product Two"
                }
            };
            return list;
        }
    }
}

