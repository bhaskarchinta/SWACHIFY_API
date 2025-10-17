using System.Threading.Tasks;

namespace Swachify.Application.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
