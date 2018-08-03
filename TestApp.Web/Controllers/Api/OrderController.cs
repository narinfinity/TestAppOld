using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestApp.Core.Entity.App;
using TestApp.Core.Entity.Domain;
using TestApp.Core.Interface.Service.App;
using TestApp.Core.Interface.Service.Domain;
using TestApp.Web.Models.OrderViewModels;

namespace TestApp.Web.Controllers.Api
{
    [Authorize(Policy = "CustomApiAuthPolicy")]
    [Produces("application/json")]
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly UserManager<User> _userManager;
        //private readonly SignInManager<User> _signInManager;
        //private readonly string _externalCookieScheme;
        private readonly IEmailSender _emailSender;
        private readonly ISmsSender _smsSender;
        private readonly ILogger _logger;
        private readonly IShoppingService _shoppingService;

        public OrderController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            ISmsSender smsSender,
            IShoppingService shoppingService,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _smsSender = smsSender;
            _shoppingService = shoppingService;
            _logger = loggerFactory.CreateLogger<OrderController>();
        }

       // [Authorize(Policy = "Reader")]
        // GET: api/Order
        [HttpGet("{id:int?}", Name = "Order")]
        public IEnumerable<Order> Get(int? id = 0)
        {
            return _shoppingService.GetOrderOrList(id.Value, _userManager.GetUserId(User));
        }

        // GET: api/Order/5
        //[HttpGet("{id:int}")]
        //public Order Get(int id)
        //{
        //    return new Order(); //TODO - _shoppingService.GetOrderById(id)
        //}

        //[Authorize(Policy = "Creator")]
        // POST: api/Order
        [HttpPost]
        public async Task<JsonResult> Post([FromBody]PaymentViewModel payment)
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult(new { errors = ModelState.SelectMany(e => e.Value.Errors.Select(er => er.ErrorMessage)) });
            };
            
            var order = new Order();
            try
            {
                var currentUser = await _userManager.GetUserAsync(User);
                order.User = currentUser;
                order.UserId = currentUser.Id;
                order.OrderedProducts = payment.OrderedProducts;
                order.Address = new Address
                {
                    Street = payment.Street,
                    City = payment.City,
                    State = payment.State,
                    Country = payment.Country,
                    PostalCode = payment.PostalCode
                };

                currentUser.FirstName = payment.FirstName;
                currentUser.LastName = payment.LastName;
                currentUser.Email = payment.Email;
                currentUser.PhoneNumber = payment.PhoneNumber;
                order = _shoppingService.SaveOrder(order);
            }
            catch (Exception e)
            {
                return new JsonResult(new { errors = new[] { e.Message, e.InnerException?.Message ?? "" } });
            }
            return new JsonResult(new { data = order });
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
