using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.Models;

namespace user_moive_search.middelware.workers.Auth
{
    public class AuthWorker : IAuthWorker
    {

        private UserManager<Users> _UserManger;
        private SignInManager<Users> _signInManager;

        public AuthWorker(UserManager<Users> userManger, SignInManager<Users> signInManager) {
            this._UserManger = userManger;
            this._signInManager = signInManager;
        }

        public async Task<SignInResult> Login(User user)
        {
            Users _appUsers = await _UserManger.FindByNameAsync(user.username);

            if (_appUsers != null)
            {
                return await _signInManager.PasswordSignInAsync(_appUsers, user.password, user.rememberme, false);
            }
            else return null;

        }

        public async Task<IdentityResult> Register(User user)
        {

            Users _appUsers = new Users
            {
                UserName = user.username
            };


            IdentityResult result = await _UserManger.CreateAsync(_appUsers, user.password);
         
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(_appUsers, false);
            }

            return result;

        }
    }
}
