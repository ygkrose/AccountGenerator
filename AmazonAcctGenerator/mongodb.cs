using MongoDB.Driver;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MongoDB.Driver.Linq;

namespace AmazonAcctGenerator
{
    public class mongodb
    {
        private MongoClient _client;
        private IMongoDatabase _database;
        private IMongoCollection<BsonDocument> _collection ;
        public mongodb()
        {
            _client =  new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
            _database = _client.GetDatabase("acct");
            _collection = _database.GetCollection<BsonDocument>("acct");
        }

        public string wrapperAccount(DataRow dr)
        {
            var lst = eatMongo("acct").ToList();
            var _bson = _collection.Find(Builders<BsonDocument>.Filter.Eq("email", dr["email"].ToString().Trim())).ToList();
            int cnt = _bson.Count();
            try
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
                
                //await collection.InsertOneAsync(document);
                //_collection.InsertOne(document);
                return "";
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        public IMongoQueryable<BsonDocument> eatMongo(string collection,string field="",string value="")
        {
            var _collection = _database.GetCollection<BsonDocument>(collection);
            IMongoQueryable<BsonDocument> _target;
            //if (field == "")
               _target = _collection.AsQueryable();
            //else
            //    _target = Builders<BsonDocument>.Filter.AnyEq(field, value);

            return _target;
        }
    }

}
