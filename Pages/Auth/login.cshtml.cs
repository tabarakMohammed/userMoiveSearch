using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.viewModel;


namespace user_moive_search.Pages
{
    public class loginModel : PageModel
    {
        private UserManager<Users> _UserManger;
        private SignInManager<Users> _signInManager;
        [BindProperty]
        public Login Model { get; set; }

        public loginModel(UserManager<Users> userManger, SignInManager<Users> signInManager)
        {
            this._UserManger = userManger;
            this._signInManager = signInManager;
        }

     

        public async Task<IActionResult> OnPostAsync(String returnUrl = null) {
            if (ModelState.IsValid)
            {

                Users _appUsers = await _UserManger.FindByNameAsync(Model.username);
                if (_appUsers != null)
                {

                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(_appUsers, Model.password, Model.rememberme, false);
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

                }
                else
                {

                    ModelState.AddModelError("", "bad Credantial");

                }
            }
            else { ModelState.AddModelError("", "bad Credantial"); }

            return Page();
            }

    }
}
