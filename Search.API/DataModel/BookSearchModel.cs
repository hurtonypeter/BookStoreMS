using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Search.API.DataModel
{
    public class BookSearchModel
    {
        [BsonId]
        public string Id { get; set; }

        public string Author { get; set; }

        public string Title { get; set; }
        
        public int FreeItemCount { get; set; }
    }
}
