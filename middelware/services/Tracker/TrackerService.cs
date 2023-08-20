using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.Configrations.MongoDB;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.middelware.workers.Tracker;

namespace user_moive_search.middelware.services.Tracker
{
    public class TrackerService
    {
        private readonly ITrackerWorker _ITrackerWorker;

        public TrackerService(ITrackerWorker iTrackerWorker)
        {
            this._ITrackerWorker = iTrackerWorker;
        }

        public Task<List<Trackerz>> FoundAllTracksSearch() {
            return _ITrackerWorker.GetAsync();
        }

        public Task<Trackerz> FoundTrackSearch(string userId, int movieId) {

            return _ITrackerWorker.GetAsync(userId, movieId);
        }

        public Task SaveUserTrackSearch(Trackerz newTrackerz) { 
      
            return _ITrackerWorker.CreateAsync(newTrackerz);
       
        }

        public Task UpdateUserTrackSearch(string userId, int movieId, Trackerz updatedTrackerz) {
           
            return _ITrackerWorker.UpdateAsync(userId, movieId, updatedTrackerz);
        
        }

        public Task DeleteUserTrackSearch(string userId, int movieId) {

            return _ITrackerWorker.RemoveAsync(userId,movieId);
        }



    }


}
