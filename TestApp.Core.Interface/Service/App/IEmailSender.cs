using System.Threading.Tasks;

namespace TestApp.Core.Interface.Service.App
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
