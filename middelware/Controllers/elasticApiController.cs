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



        [HttpGet(Name = "GetAllMovies")]
        public async Task<IActionResult> Get(string keyword)
        {
            var result = await _elasticClient.SearchAsync<Movie>(
                             s => s.Query(
                                 q => q.QueryString(
                                     d => d.Query('*' + keyword + '*')
                                 )).Size(5000));

            _logger.LogInformation("ProductsController Get - ", DateTime.UtcNow);
            return Ok(result.Documents.ToList());
        }

        [HttpPost(Name = "AddProduct")]
        public async Task<IActionResult> Post(Movie movie)
        {
            // Add product to ELS index
            /*
            var product1 = new Product
            {
                Description = "Product 1",
                Id = 1,
                Price = 200,
                Measurement = "2",
                Quantity = 90,
                ShowPrice = true,
                Title = "Nike Shoes",
                Unit = "10"
            };
            */
            // Index product dto
            await _elasticClient.IndexDocumentAsync(movie);

            _logger.LogInformation("ProductsController Get - ", DateTime.UtcNow);
            return Ok();
        }







       
    }
}
