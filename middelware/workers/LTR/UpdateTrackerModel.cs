using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_moive_search.middelware.workers.LTR
{
    public class UpdateTrackerModel
    {
        public string UserId { get; set; }
        public int MovieId { get; set; }
        public int Rank { get; set; }
    }
}
