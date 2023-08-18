using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using user_moive_search.DataAcessLayer.elasticDataModel;

namespace user_moive_search.middelware
{
    public static class ElasticSearchExtensions
    {
        public static void AddElasticsearch(
            this IServiceCollection services, IConfiguration configuration)
      
        {
            /*connect to app setting to get value, open path */
            var url = configuration["ELKConfiguration:Uri"];
            var defaultIndex = configuration["ELKConfiguration:index"];

           
            var settings = new ConnectionSettings(new Uri(url)) //.BasicAuthentication(userName, pass)
                .PrettyJson()
                .DefaultIndex(defaultIndex);

            AddDefaultMappings(settings);

            var client = new ElasticClient(settings);         
            /*Utilaze inside elasticSearch to using there services*/
            services.AddSingleton<IElasticClient>(client);

            CreateIndex(client, defaultIndex);
        }

        private static void AddDefaultMappings(ConnectionSettings settings)
        {
            settings
                .DefaultMappingFor<Movie>(m => m
                   // .Ignore(m => m.id)
                );
        }

        private static void CreateIndex(IElasticClient client, string indexName)
        {
            /*create indexing for elastic - setup to elastic*/
            var createIndexResponse = client.Indices.Create(indexName,
                index => index.Map<Movie>(x => x.AutoMap())
            );
        }


    }
}
