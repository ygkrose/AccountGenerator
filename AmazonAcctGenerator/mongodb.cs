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
        int _rows = 0;
        private Dictionary<string, BsonDocument> allaccounts;
        SortedList<string, BsonDocument> sortallaccount;
        public mongodb()
        {
            _client =  new MongoClient("mongodb://ygkroses:4rfv5tgb@ds033015.mlab.com:33015/acct");
            _database = _client.GetDatabase("acct");
            _collection = _database.GetCollection<BsonDocument>("account");
            syncReStructCollection();
        }

        private async Task asyncReStructCollection()
        {
            allaccounts = new Dictionary<string, BsonDocument>();
            var filter = new BsonDocument();
            using (var cursor = await _collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        allaccounts.Add(document["email"].ToString(), document);
                    }
                }
            }
        }

        
        private void syncReStructCollection()
        {
            var filter = new BsonDocument();
            List<BsonDocument> tmp = _collection.Find(filter).ToList();
            allaccounts = new Dictionary<string, BsonDocument>();
            //sortallaccount = new SortedList<string, BsonDocument>(tmp.Count);
            
            Parallel.ForEach(tmp, (doc) =>
            {
                
                //sortallaccount.Add(doc["email"].ToString(), doc);
                lock (allaccounts) //競爭問題有效
                {
                    allaccounts.Add(doc["email"].ToString(), doc);
                }
                //_rows++;
                //System.Threading.Interlocked.Increment(ref _rows); //避免多續處裡競爭問題(無效)
            });
          
        }

        public string wrapperAccount(DataRow dr)
        {
            BsonDocument _doc = null;
            try
            {
                if (allaccounts.ContainsKey(dr["email"].ToString().Trim()))
                {
                    _doc = allaccounts[dr["email"].ToString().Trim()];
                    return "";
                }
                else
                {

                    var document = new BsonDocument
                    {
                        {"email", dr["email"].ToString().Trim() },
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
