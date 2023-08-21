using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;
using user_moive_search.middelware.workers.ELK;

namespace user_moive_search.middelware.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class elasticApiController : ControllerBase
    {


        private readonly IElasticClient _elasticClient;
        private readonly ILogger<elasticApiController> _logger;
        private readonly ElkService _elkService;
        public elasticApiController(IElasticClient elasticClient,
                        ILogger<elasticApiController> logger,
                        ElkService elkService
                       )
         {

            _logger = logger;
            _elasticClient = elasticClient;    
            _elkService = elkService;
        }



        [Route("~/api/GetAll")]
        [HttpGet]
        public async Task<IActionResult> GetAll(string keyword)
        {


            var result = await _elkService.foundByAllData(keyword);

            if (result != null)
            {

                return Ok(result);

            }
           
            else
          
            {

                return BadRequest("False");

            }


        }

        [Produces("application/json")]
        [Route("~/api/GetOnMovieName")]
        [HttpGet]
        public async Task<IActionResult> GetOnMovieName(string keyword)
        {

            var result = await _elkService.foundByMovieName(keyword);
           
            if (result != null) {
                
                return Ok(result);
            
            } else {

               return BadRequest("False");
           
            }

        }









        [Route("~/api/AddMovie")]
        [HttpPost]
        public async Task<IActionResult> Post(Movie movie)
        {

            var result = await _elkService.saveOneDoc(movie);

            if (result)
            {

                return Ok();

            }
            else
            {

                return BadRequest("False");

            }

        }

        /*remove all data from elastic*/
        [Route("~/api/deleteIndex")]
        [HttpPost]
        public async Task<IActionResult> deleteIndex(String indexname)
        {
           //our Index name is "movie";

            var result = await _elkService.dropDataIndex(indexname);

            if (result)
            {

                return Ok();

            }
            else
            {

                return BadRequest("False");

            }

        }


        /*add json list of data to elastic*/
        [Route("~/api/addBulk")]
        [HttpPost]
        public  IActionResult addBulk(List<Movie> movies)
        {


            var result = _elkService.saveManyDocs(movies);

            if (result )
            {

                return Ok();

            }
            else
            {

                return BadRequest("False");

            }

        }


    }
}
