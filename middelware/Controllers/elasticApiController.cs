using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;

namespace user_moive_search.middelware.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class elasticApiController : ControllerBase
    {


        private readonly IElasticClient _elasticClient;
        private readonly ILogger<elasticApiController> _logger;

        public elasticApiController(IElasticClient elasticClient,
                        ILogger<elasticApiController> logger)
         {
            _logger = logger;
            _elasticClient = elasticClient;
        }



        [Route("~/api/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll(string keyword)
        {

          
            var result = await _elasticClient.SearchAsync<Movie>(
                             s => s.Query(
                                 q => q.QueryString(
                                     d => d.Query('*' + keyword + '*') 
                                 )).Size(5000));


            _logger.LogInformation("elasticApiController Get - ", DateTime.UtcNow);
            return Ok(result.Documents.ToList());
        }

        
        [Route("~/api/GetOnMovieName")]
        [HttpGet]
        public async Task<IActionResult> GetOnMovieName(string keyword)
        {

       

            var result = await _elasticClient.SearchAsync<Movie>(s => s
                        .Query(q => q
                            .Match(m => m
                                .Field(f => f.movieName)
                                .Query('*' + keyword + '*')
                            )
                        )
                    );


            _logger.LogInformation("elasticApiController ON Movie Name - ", DateTime.UtcNow);
            return Ok(result.Documents.ToList());
        }









        [Route("~/api/AddMovie")]
        [HttpPost]
        public async Task<IActionResult> Post(Movie movie)
        {    
               await _elasticClient.IndexDocumentAsync(movie);         

            _logger.LogInformation("elasticApiController post - ", DateTime.UtcNow);
            return Ok();
        }

        /*remove all data from elastic*/
        [Route("~/api/deleteIndex")]
        [HttpGet]
        public async Task<IActionResult> deleteIndex()
        {
           await this._elasticClient.Indices.DeleteAsync("movie");

            _logger.LogInformation("elasticApiController delete - ", DateTime.UtcNow);
            return Ok();
        }


        /*add json list of data to elastic*/
        [Route("~/api/addBulk")]
        [HttpPost]
        public  IActionResult addBulk(List<Movie> movie)
        {
             this._elasticClient.BulkAll(movie, 
                 m=>m.Index("movie")
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
                _logger.LogInformation("elasticApiController Bulk add - ", DateTime.UtcNow);
               
                // do something on each response e.g. write number of batches indexed to console
            });
            return  Ok();
        }

       
    }
}
