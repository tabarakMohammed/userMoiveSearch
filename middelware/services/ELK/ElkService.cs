using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;

namespace user_moive_search.middelware.workers.ELK
{
    public class ElkService 
    {
       private readonly IElkWorker _IElkWorker;
 
        public ElkService(IElkWorker IElkWorker) {
           _IElkWorker = IElkWorker;
        }


        public Task<List<Movie>> foundByAllData( String keyword)
        {
            return _IElkWorker.GetAll(keyword);
        }

         public Task<List<Movie>> foundByMovieName(String keyword)
        {
            return _IElkWorker.GetAllByMovieName(keyword);
        }

         public Task<bool> saveOneDoc(Movie movie)
        {
            return _IElkWorker.PostOne(movie);
        }

         public bool saveManyDocs(List<Movie> movies)
        {
            return _IElkWorker.PostMany(movies);
        }

          public Task<bool> dropDataIndex(String indexName)
        {
            return _IElkWorker.DeleteIndex(indexName);
        }


          public Task<bool> updateDateIndex(Movie movie)
        {
            return _IElkWorker.update(movie);
        }




    }
}
