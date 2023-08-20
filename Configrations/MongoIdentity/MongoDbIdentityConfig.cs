using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_moive_search.Configrations
{
    public class MongoDbIdentityConfig
    {
        public string name { get; set; }
        public string host { get; set; }
        public string port { get; set; }
        public string conictionString => $"mongodb://{host}:{port}";


    }
}
