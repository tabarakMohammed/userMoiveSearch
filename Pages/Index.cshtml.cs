using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.middelware.services.Tracker;

namespace user_moive_search.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public Trackerz Model { get; set; }

        
        private readonly TrackerService _TrackerService;
         
        public IndexModel( TrackerService trackerService)
        {   
            _TrackerService = trackerService;
            
        }

        /*Action for mouse clicked*/
        public async Task<IActionResult> OnPostMouseClickedAsync([FromBody] int movieId,String returnUrl = null)
            {

             Model.movieId = movieId;
          

            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _TrackerService.FoundTrackSearch(userId, Model.movieId);
          
            if (result != null)
            { 
                result.numberClicked = result.numberClicked + 1;
                await _TrackerService.UpdateUserTrackSearch(userId, Model.movieId, result);
            }
            else 
            {  
                Model.userId = userId;
                Model.numberClicked = 1;
                await _TrackerService.SaveUserTrackSearch(Model);
             }
          
            return Page();
        }

        
         public async Task<IActionResult> OnPostUserViewedAsync(String returnUrl = null)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _TrackerService.FoundTrackSearch(userId, Model.movieId);
           
            if (result != null)
            {
                result.numberViwed = ++result.numberViwed;
                await _TrackerService.UpdateUserTrackSearch(userId, Model.movieId, result);
            }
            else 
            {

                Model.userId = userId;
                Model.numberViwed = 1;
                await _TrackerService.SaveUserTrackSearch(Model);
             }
           
            return Page();
        }

        /*on searched */
          public async Task<IActionResult> OnPostUserSearchedAsync(String returnUrl = null)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _TrackerService.FoundTrackSearch(userId, Model.movieId);
           
            if (result != null)
            {
                result.numberSearched = ++result.numberSearched;
                await _TrackerService.UpdateUserTrackSearch(userId, Model.movieId, result);
            }
            else 
            {

                Model.userId = userId;
                Model.numberSearched = 1;
                await _TrackerService.SaveUserTrackSearch(Model);
             }
           
            return Page();
        }




    }
}
