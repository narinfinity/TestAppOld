using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TestApp.Web.Models.Authorization
{
    public class LogoutViewModel
    {
        [BindNever]
        public string RequestId { get; set; }
    }
}
