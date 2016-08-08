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
            dorestruct();
        }

        private void dorestruct()
        {
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
                        _rows++;
                        System.Threading.Interlocked.Increment(ref _rows); //避免多續處裡競爭問題
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

        public string wrapperAccount(DataRow dr, DataTable dtb)
        {
            try
            {
                List<BsonDocument> ba = new List<BsonDocument>();
                foreach (DataRow _row in dtb.Rows)
                {
                    ba.Add(new BsonDocument
                                {
                                    {"ritem",_row["itemno"].ToString().Trim() },
                                    {"rdate",_row["rvtime"].ToString().Trim()  },
                                    {"rtype",_row["rvtype"].ToString().Trim() },
                                    {"status",( _row["success"].ToString()=="False"? "fail" : "success") },
                                    {"reviewer",_row["reviewer"].ToString().Trim()  }
                                });
                }

                var document = new BsonDocument
                    {
                        {"email", dr["email"].ToString().Trim() },
                        {"pwd", dr["pwd"].ToString().Trim() },
                        {"status", dr["status"].ToString().Trim()=="New"?"Created":dr["status"].ToString().Trim()},
                        {"purchase", new BsonDocument
                            {
                                {"pdate", dr["pdate"].ToString().Trim() },
                                {"pname",  dr["rcvname"].ToString().Trim() },
                                {"ptel",  dr["tel"].ToString().Trim() },
                                {"pitem",  dr["pitem"].ToString().Trim() },
                                {"pcardno",  dr["cardno"].ToString().Trim() }
                            }
                        },
                        {"review" , new BsonArray(ba)},
                        {"vpn", string.IsNullOrEmpty(dr["nordVPN"].ToString())?"":dr["nordVPN"].ToString().Trim()}
                    };

                if (allaccounts.ContainsKey(dr["email"].ToString().Trim()))
                {
                    BsonDocument _doc = null;
                    _doc = allaccounts[dr["email"].ToString().Trim()];
                    var filter = Builders<BsonDocument>.Filter.Eq("_id", _doc["_id"]);
                    //var update = Builders<BsonDocument>.Update
                    //        .Set("cuisine", "American (New)")
                    //        .CurrentDate("lastModified");
                    //_collection.UpdateOne(filter, update);
                    //_collection.ReplaceOneAsync(filter, document); //not use for free account
                    _collection.ReplaceOne(filter, document);
                }
                else
                {
                    //await collection.InsertOneAsync(document);//not use for free account
                    _collection.InsertOne(document);
                }
                return "";
            }
            catch (Exception err)
            {
                return err.Message;
            }
        }

        public List<BsonDocument> getDocForSync()
        {
            _collection= _database.GetCollection<BsonDocument>("account");
            var filter = Builders<BsonDocument>.Filter.Ne("status", "Created");
            return _collection.Find(filter).ToList();
        }
    }

}
