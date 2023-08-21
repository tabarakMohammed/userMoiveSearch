using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.middelware.workers.LTR;

namespace user_moive_search.middelware.services.LTR
{
    public class LTRService
    {
        private readonly ILTRworker _ILTRworker;

        public LTRService(ILTRworker lTRworker)
        {
           this._ILTRworker = lTRworker;
        }
    }
}
