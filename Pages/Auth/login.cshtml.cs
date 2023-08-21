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
    public class loginModel : PageModel
    {
     
        private AuthService _authService;
    
        [BindProperty]
        public User Model { get; set; }

        public loginModel( AuthService authService)
        {    
            this._authService = authService;
        }

     

        public async Task<IActionResult> OnPostAsync(String returnUrl = null) {
            if (ModelState.IsValid)
            {

                Microsoft.AspNetCore.Identity.SignInResult  result = await _authService.Login(Model);

               
                   
                    if (result.Succeeded)
                    {

                        if (returnUrl == null || returnUrl == "/")
                        {

                            return RedirectToPage("/Index");

                        }
                        else
                        {
                            return RedirectToPage("returnUrl");
                        }
                    }

                
              
            } else { ModelState.AddModelError("", "bad Credantial"); }

            return Page();
            }

    }
}
