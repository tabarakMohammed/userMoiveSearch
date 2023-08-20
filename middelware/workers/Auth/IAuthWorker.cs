using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.Models;

namespace user_moive_search.middelware.workers.Auth
{
   public interface IAuthWorker
    {
        public Task<IdentityResult> Register(User user);
        public Task<SignInResult> Login(User user);
    }
}
