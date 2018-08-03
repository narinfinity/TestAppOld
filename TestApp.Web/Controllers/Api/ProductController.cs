using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestApp.Core.Entity.Domain;
using TestApp.Core.Interface.Service.Domain;

namespace TestApp.Web.Controllers.Api
{
    //[Authorize(AuthenticationSchemes = "Cookies,Bearer")]
    [Authorize(Policy = "CustomApiAuthPolicy")]
    [Produces("application/json")]
    [Route("api/Product")]
    public class ProductController : Controller
    {
        private readonly IProductStoreService _productStoreService;

        public ProductController(
            IProductStoreService productStoreService)
        {
            _productStoreService = productStoreService;
        }

        //[Authorize(Policy = "Reader")]
        // GET: api/Product
        [HttpGet("{categoryId:int?}", Name = "Product")]
        public IEnumerable<Product> Get(int? categoryId = 0)
        {
            return _productStoreService.GetProductsByCategory(new Category { Id = categoryId.Value });
        }

        // GET: api/Product/5
        //[HttpGet("{id:int}", Name = "Product")]
        //public Product Get(int id)
        //{
        //    return new Product();//TODO _productStoreService.GetProductById(id)
        //}

        //[Authorize(Policy = "Creator")]
        // POST: api/Product
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
