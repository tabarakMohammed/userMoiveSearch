using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.Models;

namespace user_moive_search.middelware.workers.LTR
{
  public interface ILTRworker
    {
        public string TrainDataRank(Trackerz trackerz);
        public string UpdateRankELk(UpdateTrackerModel updateTrackerModel);
    }
}
