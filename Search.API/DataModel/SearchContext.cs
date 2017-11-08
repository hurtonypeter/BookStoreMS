using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.API.DataModel
{
    public class SearchContext
    {
        private readonly IMongoDatabase database;

        public SearchContext()
        {
            var client = new MongoClient("mongodb://localhost");
            if (client != null)
                database = client.GetDatabase("SearchDb");
        }

        public IMongoCollection<BookSearchModel> Books
        {
            get { return database.GetCollection<BookSearchModel>("Books"); }
        }
    }
}
