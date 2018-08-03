using System.Threading.Tasks;

namespace TestApp.Core.Interface.Service.App
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
