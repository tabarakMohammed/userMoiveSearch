using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;

namespace user_moive_search.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IElasticClient _elasticClient;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger, IElasticClient elasticClient)
        {
            _logger = logger;
            _elasticClient = elasticClient;
        }

        public async Task<IActionResult> Find(string query, int page = 1, int pageSize = 5)
        {
            var response = await _elasticClient.SearchAsync<Movie>
        (
                s => s.Query(q => q.QueryString(d => d.Query(query)))
                    .From((page - 1) * pageSize)
                    .Size(pageSize));

            if (!response.IsValid)
            {
                // We could handle errors here by checking response.OriginalException 
                //or response.ServerError properties
                _logger.LogError("Failed to search documents");
                return RedirectToPage("Index", new Movie[] { });
            }
            /*
            if (page > 1)
            {
                ViewData["prev"] = GetSearchUrl(query, page - 1, pageSize);
            }

            if (response.IsValid && response.Total > page * pageSize)
            {
                ViewData["next"] = GetSearchUrl(query, page + 1, pageSize);
            }
            */
            return RedirectToPage("Index", response.Documents);
        }
    }
}
