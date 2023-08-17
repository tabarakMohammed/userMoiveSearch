using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using user_moive_search.DataAcessLayer.Models;

namespace user_moive_search.Pages
{
    public class logotModel : PageModel
    {

        
        private SignInManager<Users> _signInManager;

        public logotModel(SignInManager<Users> signInManager)
        {
           
            this._signInManager = signInManager;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Login");
        }
    }
}
