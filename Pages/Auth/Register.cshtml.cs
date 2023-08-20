using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.middelware.services.Auth;

namespace user_moive_search.Pages
{
    public class RegisterModel : PageModel
    {
     
     
        private AuthService _authService;
        [BindProperty]
        public User Model { get; set; }
     
        public RegisterModel(AuthService authService)
        {    
            this._authService = authService;
        }
     


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
             

                IdentityResult result = await _authService.Register(Model);
                if (result.Succeeded)
                {            
                   return RedirectToPage("/Index");   
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                }

            }

            return Page();
        }


    }
}
