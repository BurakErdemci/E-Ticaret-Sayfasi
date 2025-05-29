using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public interface IAccountServices
    {
        Task<IdentityResult> RegisterAsync(string firstName, string lastName,DateTime birthDate, string email, string password);
        Task<SignInResult>LoginAsync(string email, string password, bool rememberMe);
        Task LogOutAsync();
    }
}
