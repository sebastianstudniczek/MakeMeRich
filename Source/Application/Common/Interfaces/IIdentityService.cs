using System.Threading.Tasks;
using MakeMeRich.Application.Common.Models;

namespace MakeMeRich.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> LoginUserAsync(string email, string password);
        Task<AuthenticationResult> CreateUserAsync(string userName, string password);
    }
}
