using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApp.Core.Entity.App;
using TestApp.Core.Entity.Domain;
using TestApp.Core.Interface.Service.App;
using TestApp.Core.Interface.Service.Domain;

namespace TestApp.Web.Controllers.Api
{
    [Authorize(Policy = "CustomApiAuthPolicy")]
    [Produces("application/json")]
    [Route("api/Category")]
    public class CategoryController : Controller
    {
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        //private readonly string _externalCookieScheme;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IProductStoreService _productStoreService;

        public CategoryController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,            
            IEmailSender emailSender,
            ISmsSender smsSender,
            IProductStoreService productStoreService,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _productStoreService = productStoreService;
            _logger = loggerFactory.CreateLogger<CategoryController>();
        }

        //[Authorize(Policy = "Reader")]
        // GET: api/Categories
        [HttpGet("{id:int?}", Name = "Category")]
        public IEnumerable<Category> Get(int? id = 0)
        {
            return _productStoreService.GetCategoryOrList(id.Value);
        }

        //GET: api/Category/5
        //[HttpGet("{id:int}", Name = "Category")]
        //public Category Get(int id)
        //{
        //    return new Category(); //TODO - _productStoreService.GetCategoryById(id)
        //}

        //[Authorize(Policy = "Creator")]
        // POST: api/Category
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
