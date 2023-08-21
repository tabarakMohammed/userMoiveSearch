using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_moive_search.DataAcessLayer.Models
{
    public class Trackerz
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string userId { get; set; }
        public int movieId { get; set; }
        public int numberClicked { get; set; }
        public int numberSearched { get; set; }
        public int numberViwed { get; set; }
    }
}



