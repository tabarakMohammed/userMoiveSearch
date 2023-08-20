using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.middelware.workers.Auth;

namespace user_moive_search.middelware.services.Auth
{
    public class AuthService
    {
        private readonly IAuthWorker _iAuthWorker;

        public AuthService(IAuthWorker iAuthWorker) {
            this._iAuthWorker = iAuthWorker;
        }


        public Task<IdentityResult> Register(User user) {
            return _iAuthWorker.Register(user);
        }
        public Task<SignInResult> Login(User user) {
            return _iAuthWorker.Login(user);
        }




    }
}
