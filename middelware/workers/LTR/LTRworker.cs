using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;
using user_moive_search.DataAcessLayer.Models;
using user_moive_search.middelware.workers.ELK;

namespace user_moive_search.middelware.workers.LTR
{
    public class LTRworker:ILTRworker
    {
        private readonly ILogger<LTRworker> _logger;
        private readonly ElkService _ElkService;
        public LTRworker(ILogger<LTRworker> logger, ElkService elkService)
        {
            _logger = logger;
            _ElkService = elkService;
        }

        public string TrainDataRank(Trackerz trackerz)
        {
           int Clicked = trackerz.numberClicked;
           int Viwed = trackerz.numberViwed;
           int Searched =  trackerz.numberSearched;
            
            /* data train class gave me rank*/

            throw new NotImplementedException();
        }

        public string UpdateRankELk(UpdateTrackerModel updateTrackerModel)
        {
            /*update node with user rank*/
              Movie movie = new Movie();
            _ElkService.updateDateIndex(movie);
            throw new NotImplementedException();

        }


    }
}
