using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.Models;

namespace user_moive_search.middelware.workers.Tracker
{
    public interface ITrackerWorker
    {
        public Task<List<Trackerz>> GetAsync();

        public Task<Trackerz> GetAsync(string userId, int movieId);

        public Task CreateAsync(Trackerz newTrackerz);

        public Task UpdateAsync(string userId, int movieId, Trackerz updatedTrackerz);

        public Task RemoveAsync(string userId, int movieId);
        
    }
}
