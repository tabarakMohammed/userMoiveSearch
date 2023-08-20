using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;

namespace user_moive_search.middelware.workers.ELK
{
   public interface IElkWorker
    {
        public Task<List<Movie>> GetAll(String movieName);
        public Task<List<Movie>> GetAllByMovieName(String movieName);
        public Task<bool> PostOne(Movie movie);
        public bool PostMany(List<Movie> movies);

        public Task<bool> DeleteIndex(String IndexName);

    }
}
