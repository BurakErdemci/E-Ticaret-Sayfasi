using Core.Abstract;
using Core.Concretes.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AccountServices:IAccountServices
    {
        private readonly UserManager<Member> userManager;
        private readonly SignInManager<Member> signInManager;
        public AccountServices(UserManager<Member> userManager ,SignInManager<Member> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<SignInResult> LoginAsync(string email, string password, bool rememberMe)
        {
            return await signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure:false);    
        }

        public Task LogOutAsync()
        {
            return signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(string firstName, string lastName, DateTime birthDate, string email, string password)
        {
            var member = new Member
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,

                DateOfBirth = birthDate,
                UserName = email
            };
            return await userManager.CreateAsync(member,password);
        }
    }
}
