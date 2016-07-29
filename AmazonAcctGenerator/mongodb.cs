using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AmazonAcctGenerator
{
    public class mongodb
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        public mongodb()
        {
            _client =  new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
            _database = _client.GetDatabase("acct");
        }

        public void wrapperAccount(DataRow dr)
        {
            var document = new BsonDocument
            {
                {"eamil", dr["email"].ToString().Trim() },
                {"pwd", dr["pwd"].ToString().Trim() },
                {"status", dr["status"].ToString().Trim()},
                {"purchase", new BsonDocument
                    {
                        {"pdate", dr["pdate"].ToString().Trim() },
                        {"pname",  dr["rcvname"].ToString().Trim() },
                        {"ptel",  dr["tel"].ToString().Trim() },
                        {"pitem",  dr["pitem"].ToString().Trim() },
                        {"pcardno",  dr["cardno"].ToString().Trim() }
                    }
                },
                {"review" , new BsonArray
                    {
                        new BsonDocument
                        {
                            {"ritem","" },
                            {"rdate","" },
                            {"rtype","" },
                            {"status","" },
                            {"reviewer","" }
                        }
                    }
                }
            };
            var collection = _database.GetCollection<BsonDocument>("acct");
            //await collection.InsertOneAsync(document);
            collection.InsertOne(document);
        }
    }

}
