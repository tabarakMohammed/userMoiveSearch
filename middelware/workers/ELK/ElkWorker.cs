using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;

namespace user_moive_search.middelware.workers.ELK
{
    public class ElkWorker : IElkWorker
    {


        private readonly IElasticClient _elasticClient;
        private readonly ILogger<ElkWorker> _logger;

        public ElkWorker( IElasticClient elasticClient, ILogger<ElkWorker> logger)
        {
            this._logger = logger;
            this._elasticClient = elasticClient;
           }


        public async Task<List<Movie>> GetAll(string keyword)
        {
            try
            {
                var result = await _elasticClient.SearchAsync<Movie>(
                                s => s.Query(
                                    q => q.QueryString(
                                        d => d.Query('*' + keyword + '*')
                                    )).Size(5000));
                
                return result.Documents.ToList();
            }
            catch (Exception _exception) {     
                
                _logger.LogError(" ElkWorker line 38"+ DateTime.UtcNow, _exception.StackTrace);
                return null;
            }
            
        }

        public async Task<List<Movie>> GetAllByMovieName(string movieName)
        {

            string pattern = "@[^\\s\\p{L}\\p{N}]";
            try
            {
               
                string valdKey = Regex.Replace(movieName, pattern, " ");

                var result = await _elasticClient.SearchAsync<Movie>(s => s
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.movieName)
                                .Query('*' + valdKey + '*')  
                            )
                        )
                    );
               
                return result.Documents.ToList();
            }
            catch (Exception _exception)
            {
                

               _logger.LogError(" ElkWorker line 62" + DateTime.UtcNow , _exception.StackTrace);
                return null;
            }
          
         
        }

        public async Task<bool> PostOne(Movie movie)
        {
            try
            {
              
                await _elasticClient.IndexDocumentAsync(movie);
              
                return true;
            }
            catch (Exception _exception)
            {
               _logger.LogError(" ElkWorker line 81" + DateTime.UtcNow, _exception.StackTrace);
                return false;
            }

      
        }

        public bool PostMany(List<Movie> movies)
        {
            try
            {
                this._elasticClient.BulkAll(movies,
               m => m.Index("movie")
              // how long to wait between retries
              .BackOffTime("30s")
              // how many retries are attempted if a failure occurs
              .BackOffRetries(2)
              // refresh the index once the bulk operation completes
              .RefreshOnCompleted()
              /*
              // how many concurrent bulk requests to make
              .MaxDegreeOfParallelism(Environment.ProcessorCount)
              // number of items per bulk request
              .Size(1000)
              */
          ).Wait(TimeSpan.FromMinutes(15), next =>
          {   
                // do something on each response e.g. write number of batches indexed to console
          });

                return true;
            }
            catch (Exception _exception)
            {
               _logger.LogError(" ElkWorker line 115" + DateTime.UtcNow, _exception.StackTrace);
                return false;
            }

        }

        public async Task<bool> DeleteIndex(string IndexName)
        {
            try {
               var result = await this._elasticClient.Indices.DeleteAsync(IndexName);
                /*check result and return*/
                return true;
            }
            catch (Exception _exception)
            {
                _logger.LogError(" ElkWorker line 130"+ DateTime.UtcNow, _exception.StackTrace);
                return false;
            }
        }

     
        public Task<bool> update(Movie movie)
        {
            throw new NotImplementedException();
        }



    }
}
