using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace user_moive_search.DataAcessLayer.Models
{
    [CollectionName("Users")]
    public class Users:MongoIdentityUser<Guid>
    {
        
    }
}
