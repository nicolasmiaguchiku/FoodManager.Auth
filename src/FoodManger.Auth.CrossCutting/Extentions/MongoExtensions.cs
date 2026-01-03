using FoodManager.Auth.CrossCutting.Models;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FoodManager.Auth.CrossCutting.Extentions
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongo(this IServiceCollection services, MongoSettings mongoSettings)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard));

            var clientMongo = MongoClientSettings.FromConnectionString(mongoSettings.ConnectionString);
            var mongoClient = new MongoClient(clientMongo);
            services.AddSingleton<IMongoClient>(_ => mongoClient);

            services.AddSingleton(sp =>
            {
                var mongoClient = sp.GetService<IMongoClient>();
                var database = mongoClient!.GetDatabase(mongoSettings.DataBaseName);
                return database;
            });

            return services;
        }
    }
}