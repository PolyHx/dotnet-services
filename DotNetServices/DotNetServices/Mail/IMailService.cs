using System.Threading.Tasks;
using PolyHxDotNetServices.Mail.Inputs;

namespace PolyHxDotNetServices.Mail
{
    public interface IMailService
    {
        Task<bool> SendEmail(SendMailInput input);
    }
}