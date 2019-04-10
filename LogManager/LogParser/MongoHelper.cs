using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LogParser
{
    public class MongoHelper
    {
        #region 构造函数
        public MongoHelper(string server, int port, string db)
        {
            ServerHostName = server;
            Port = port;
            DB = db;
            Initialize();
        }

        public MongoHelper(string db)
        {
            ServerHostName = "127.0.0.1";
            Port = 27017;
            DB = db;
            Initialize();
        }

        public MongoHelper(string server, string db)
        {
            ServerHostName = server;
            DB = db;
            Port = 27017;
            Initialize();
        }

        public MongoHelper(int port, string db)
        {
            ServerHostName = "127.0.0.1";
            Port = port;
            DB = db;
            Initialize();
        }
        #endregion

        private string ServerHostName { get; set; }

        private int Port { get; set; }

        private string DB { get; set; }

        MongoClient Client { get; set; }

        public FilterDefinitionBuilder<T> GetFilter<T>()
        {
            return Builders<T>.Filter;
        }

        public UpdateDefinitionBuilder<T> GetUpdate<T>()
        {
            return Builders<T>.Update;
        }


        public IMongoDatabase DataBase { get; set; }

        private void Initialize()
        {
            Client = new MongoClient(string.Format("mongodb://{0}:{1}", ServerHostName, Port));

            DataBase = Client.GetDatabase(DB);
        }

        public  void InsertOne<T>(string collectionName, T entity) where T : class
        {
            IMongoCollection<T> collections = DataBase.GetCollection<T>(collectionName);
            collections.InsertOne(entity);
        }

        public  void InsertAll<T>(string collectionName, IEnumerable<T> entity) where T : class
        {
            IMongoCollection<T> collections = DataBase.GetCollection<T>(collectionName);
            collections.InsertMany(entity);
        }

        public T GetOne<T>(string collectionName, FilterDefinition<T>filter)
        {
            filter = filter ?? Builders<T>.Filter.Empty;

            var collection = DataBase.GetCollection<T>(collectionName);

            return collection.Find(filter).FirstOrDefault();
        }

        public IList<T> GetAll<T>(string collectionName, FilterDefinition<T> filter)
        {
            var collection = DataBase.GetCollection<T>(collectionName);
            return collection.Find(filter).ToList();
        }

        public void UpdateOne<T>(string collectionName, FilterDefinition<T> filter,UpdateDefinition<T>update)
        {
            var collection = DataBase.GetCollection<T>(collectionName);
            collection.UpdateOne(filter, update);
        }

        public void UpdateAll<T>(string collectionName, FilterDefinition<T> filter, UpdateDefinition<T> update)
        {
            var collection = DataBase.GetCollection<T>(collectionName);
            collection.UpdateMany(filter, update);
        }

        public void DeleteOne<T>(string collectionName, FilterDefinition<T> filter)
        {
            var collection = DataBase.GetCollection<T>(collectionName);
            collection.DeleteOne(filter);
        }

        public void DeleteMany<T>(string collectionName, FilterDefinition<T> filter)
        {

                var collection = DataBase.GetCollection<T>(collectionName);

                collection.DeleteMany(filter??Builders<T>.Filter.Empty);

        }

        public long CountQuery<T>(string collectionName, FilterDefinition<T> filter)
        {
            var collection = DataBase.GetCollection<T>(collectionName);
            return collection.Find(filter).CountDocuments();
        }
    }
}
