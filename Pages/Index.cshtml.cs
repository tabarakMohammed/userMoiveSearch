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


        public async Task<IActionResult> OnPostAsync()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Model.userId = userId;
            Model.numberClicked = 1;
            
            await _TrackerService.SaveUserTrackSearch(Model);
            
            return Page();
        }

    }
}
