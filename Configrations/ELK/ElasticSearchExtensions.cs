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
            /*
             * create indexing for elastic and improve search method - setup to elastic
           
            */

            var createIndexResponse = client.Indices.Create(indexName, c => c
                .Settings(s => s
                    .Analysis(a => a
                        .Analyzers(an => an
                            .Custom("trigram", analyzer => analyzer
                                .Tokenizer("standard")
                                .Filters("lowercase", "shingle", "asciifolding")
                                .CharFilters()
                            )
                            .Custom("reverse", analyzer => analyzer
                                .Tokenizer("standard")
                                .Filters("lowercase", "reverse", "asciifolding")
                            )
                        )
                        .TokenFilters(tf => tf
                            .Shingle("shingle", shingle => shingle
                                .MinShingleSize(2)
                                .MaxShingleSize(3)
                            )
                        )
                    )
                )
                .Map<Movie>(m => m
                    .Properties(p => p
                        .Text(t => t
                            .Name(n => n.movieName)
                            .Fields(f => f
                                .Text(tf => tf
                                    .Name("trigram")
                                    .Analyzer("trigram")
                                )
                                .Text(tf => tf
                                    .Name("reverse")
                                    .Analyzer("reverse")
                                )
                            )
                        )
                    )
                )
            );


        }


    }
}
