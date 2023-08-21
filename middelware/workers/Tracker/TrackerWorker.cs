using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.Configrations.MongoDB;
using user_moive_search.DataAcessLayer.Models;

namespace user_moive_search.middelware.workers.Tracker
{
    public class TrackerWorker : ITrackerWorker
    {
            private readonly IMongoCollection<Trackerz> _trackerCollection;
            private readonly ILogger<TrackerWorker> _logger;

        public TrackerWorker(
            ILogger<TrackerWorker> logger,
           IOptions<UserTrackerDbSettings> bookStoreDatabaseSettings)
            {
                var mongoClient = new MongoClient(
                    bookStoreDatabaseSettings.Value.ConnectionString);

                var mongoDatabase = mongoClient.GetDatabase(
                    bookStoreDatabaseSettings.Value.DatabaseName);

                _trackerCollection = mongoDatabase.GetCollection<Trackerz>(
                    bookStoreDatabaseSettings.Value.DatabaseName);

                  _logger = logger;
            }




        public async Task<List<Trackerz>> GetAsync()
        {
            return await _trackerCollection.Find(_ => true).ToListAsync();
        }
        public async Task<Trackerz> GetAsync(string userId, int movieId)
        {
            return await _trackerCollection.Find(
                trackz =>
                (trackz.userId.Equals(userId) &&
                 trackz.movieId == movieId)
            ).FirstOrDefaultAsync();
        }
        public async Task CreateAsync(Trackerz newTrackerz)
        {
            await _trackerCollection.InsertOneAsync(newTrackerz);
        }
        public async Task UpdateAsync(string userId, int movieId, Trackerz updatedTrackerz)
        {
            await _trackerCollection.ReplaceOneAsync(trackz =>
              (trackz.userId.Equals(userId) &&
               trackz.movieId == movieId), updatedTrackerz);
        }
        public async Task RemoveAsync(string userId, int movieId)
        {
            await _trackerCollection.DeleteOneAsync(trackz =>
            (trackz.userId.Equals(userId) &&
               trackz.movieId == movieId));
        }




    }
}
