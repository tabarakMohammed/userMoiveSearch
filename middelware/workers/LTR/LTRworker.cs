using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_moive_search.middelware.workers.LTR
{
    public class LTRworker:ILTRworker
    {
        private readonly ILogger<LTRworker> _logger;
        public LTRworker(ILogger<LTRworker> logger)
        {
            _logger = logger;
        }


    }
}
