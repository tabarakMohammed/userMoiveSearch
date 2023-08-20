using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.middelware.services.Auth;
using user_moive_search.viewModel;


namespace user_moive_search.Pages
{
    public class loginModel : PageModel
    {
     
        private AuthService _authService;
    
        [BindProperty]
        public Login Model { get; set; }

        public loginModel( AuthService authService)
        {    
            this._authService = authService;
        }

     

        public async Task<IActionResult> OnPostAsync(String returnUrl = null) {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.username = Model.username;
                user.password = Model.password;
                user.rememberme = Model.rememberme;

                Microsoft.AspNetCore.Identity.SignInResult  result = await _authService.Login(user);

               
                   
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
