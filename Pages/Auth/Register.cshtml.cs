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
    public class RegisterModel : PageModel
    {
     
        private UserManager<Users> _UserManger;
        private SignInManager<Users> _signInManager;
        [BindProperty]
        public User Model { get; set; }
     
        public RegisterModel(UserManager<Users> userManger, SignInManager<Users> signInManager)
        {
            this._UserManger = userManger;
            this._signInManager = signInManager;
        }
     


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Users _appUsers = new Users
                {
                    UserName = Model.username
                };

                IdentityResult result = await _UserManger.CreateAsync(_appUsers, Model.password);
                if (result.Succeeded)
                {
                   await _signInManager.SignInAsync(_appUsers, false);
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
